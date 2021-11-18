using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChemStoreWebApp.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class chemstoreContext : DbContext
    {
        IHttpContextAccessor httpContext;
        public chemstoreContext()
        {
        }

        public chemstoreContext(DbContextOptions<chemstoreContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            httpContext = httpContextAccessor;
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Chemical> Chemical { get; set; }
        public virtual DbSet<ChemicalHazards> ChemicalHazards { get; set; }
        public virtual DbSet<Container> Container { get; set; }
        public virtual DbSet<Hazard> Hazard { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=127.0.0.1; Database=chemstore; user=root; password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.AccountId).HasColumnName("Account_ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Chemical>(entity =>
            {
                entity.HasKey(e => e.CasNumber)
                    .HasName("PRIMARY");

                entity.ToTable("chemical");

                entity.Property(e => e.CasNumber)
                    .HasColumnName("CAS_Number")
                    .HasMaxLength(255);

                entity.Property(e => e.CatalogNumber)
                    .IsRequired()
                    .HasColumnName("Catalog_Number")
                    .HasMaxLength(255);

                entity.Property(e => e.ChemicalName)
                    .IsRequired()
                    .HasColumnName("Chemical_Name")
                    .HasMaxLength(255);

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ChemicalHazards>(entity =>
            {
                entity.HasKey(e => new { e.CasNumber, e.HazardId })
                    .HasName("PRIMARY");

                entity.ToTable("chemical_hazards");

                entity.HasIndex(e => e.HazardId)
                    .HasName("Hazard_ID");

                entity.Property(e => e.CasNumber)
                    .HasColumnName("CAS_Number")
                    .HasMaxLength(255);

                entity.Property(e => e.HazardId)
                    .HasColumnName("Hazard_ID")
                    .HasMaxLength(255);

                entity.HasOne(d => d.CasNumberNavigation)
                    .WithMany(p => p.ChemicalHazards)
                    .HasForeignKey(d => d.CasNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("chemical_hazards_ibfk_1");

                entity.HasOne(d => d.Hazard)
                    .WithMany(p => p.ChemicalHazards)
                    .HasForeignKey(d => d.HazardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("chemical_hazards_ibfk_2");
            });

            modelBuilder.Entity<Container>(entity =>
            {
                entity.ToTable("container");

                entity.HasIndex(e => e.CasNumber)
                    .HasName("CAS_Number");

                entity.HasIndex(e => e.RoomId)
                    .HasName("Room_ID");

                entity.HasIndex(e => e.SupervisorId)
                    .HasName("Supervisor_ID");

                entity.Property(e => e.ContainerId)
                    .HasColumnName("Container_ID")
                    .HasColumnType("bigint unsigned");

                entity.Property(e => e.Amount).HasComment("Amount of the unit (200 g)");

                entity.Property(e => e.CasNumber)
                    .IsRequired()
                    .HasColumnName("CAS_Number")
                    .HasMaxLength(255)
                    .HasComment("FK(Chemical.id)");

                entity.Property(e => e.RoomId)
                    .HasColumnName("Room_ID")
                    .HasMaxLength(255);

                entity.Property(e => e.SupervisorId)
                    .HasColumnName("Supervisor_ID")
                    .HasComment("FK(Supervisor.ID)");

                entity.Property(e => e.Unit).HasComment("ex. ML, g, gallons");

                entity.HasOne(d => d.CasNumberNavigation)
                    .WithMany(p => p.Container)
                    .HasForeignKey(d => d.CasNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("container_ibfk_1");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Container)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("container_ibfk_2");

                entity.HasOne(d => d.Supervisor)
                    .WithMany(p => p.Container)
                    .HasForeignKey(d => d.SupervisorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("container_ibfk_3");
            });

            modelBuilder.Entity<Hazard>(entity =>
            {
                entity.ToTable("hazard");

                entity.Property(e => e.HazardId)
                    .HasColumnName("Hazard_ID")
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PRIMARY");

                entity.ToTable("location");

                entity.Property(e => e.RoomId)
                    .HasColumnName("Room_ID")
                    .HasMaxLength(255);

                entity.Property(e => e.BuildingName)
                    .IsRequired()
                    .HasColumnName("Building_Name");

                entity.Property(e => e.RoomNumber)
                    .IsRequired()
                    .HasColumnName("Room_Number")
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<Role> Role { get; set; } // TODO TEMPORARY

        public void LogChanges()
        {
            var changeList = from e in ChangeTracker.Entries()
                             where e.State != EntityState.Detached && e.State != EntityState.Unchanged
                             && (e.Entity is Container || e.Entity is Account)
                             select e;

            List<Log> logList = new List<Log>();

            string uname = httpContext.HttpContext.User.Identity.Name;
            int userId = (from a in Account
                          where a.Email == uname
                          select a.AccountId).FirstOrDefault();

            foreach (var entity in changeList)
            {
                Log newLog = new Log
                {
                    DateTime = DateTime.Now,
                    UserID = userId
                };
                //Container changes
                if (entity.Entity is Container container)
                {
                    newLog.ContainerID = container.ContainerId;
                    switch (entity.State)
                    {
                        case EntityState.Deleted:
                            newLog.Action = ((int)Actions.ContainerRemoved);
                            break;

                        case EntityState.Added:
                            newLog.Action = ((int)Actions.ContainerAdded);
                            break;

                        case EntityState.Modified:
                            var modifiedFields = Entry(container).Properties.Where(a => a.IsModified);
                            if (modifiedFields.Any(a => a.Metadata.Name == "RoomId"))
                            {
                                newLog.Action = ((int)Actions.ContainerTransferred);
                                //other edits besides Location made
                                if (modifiedFields.Any(a => a.Metadata.Name != "RoomId"))
                                {
                                    logList.Add(new Log
                                    {
                                        DateTime = DateTime.Now,
                                        ContainerID = container.ContainerId,
                                        Action = ((int)Actions.ContainerEdited)
                                    });
                                }
                            }
                            else
                            {
                                newLog.Action = ((int)Actions.ContainerEdited);
                            }
                            break;
                    }
                }
                else
                //Account changes
                {
                    //if they are added and they aren't just a member, or their new role is less their old role, they have been promoted
                    if ((entity.State == EntityState.Added && ((int)entity.CurrentValues["Role"]) != ((int)Roles.Member)) ||
                        (entity.State == EntityState.Modified && (((int)entity.CurrentValues["Role"]) < ((int)entity.OriginalValues["Role"]))))
                    {
                        newLog.Action = ((int)Actions.UserPromoted);
                        newLog.Description = $"Promoted User {entity.CurrentValues["Name"]} - {entity.CurrentValues["Email"]} to {EnumHelper.GetDisplayValue((Roles)entity.CurrentValues["Role"])}";
                    }
                    else if (entity.State == EntityState.Deleted ||
                      (entity.State == EntityState.Modified && (((int)entity.CurrentValues["Role"]) > ((int)entity.OriginalValues["Role"]))))
                    {
                        newLog.Action = ((int)Actions.UserDemoted);
                        newLog.Description = $"Demoted User {entity.CurrentValues["Name"]} - {entity.CurrentValues["Email"]} to {(entity.State == EntityState.Deleted ? "Member" : EnumHelper.GetDisplayValue((Roles)entity.CurrentValues["Role"]))}";
                    }
                }

                logList.Add(newLog);
            }

            Log.AddRange(logList);
        }
        public override int SaveChanges()
        {
            LogChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            LogChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
