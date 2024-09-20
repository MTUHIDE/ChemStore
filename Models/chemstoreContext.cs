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

        //NEW ADDITIONS:
        public virtual DbSet<ContainerHazards> ContainerHazards { get; set; }
        public virtual DbSet<X_Container> X_Container { get; set; }
        public virtual DbSet<ContainerChemicals> ContainerChemicals { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<HazardPictogram> HazardPictogram { get; set; }
        public virtual DbSet<HazardPrecaution> HazardPrecaution { get; set; }
        public virtual DbSet<HazardStatement> HazardStatement { get; set; }
        public virtual DbSet<X_Location> X_Location { get; set; }
        public virtual DbSet<LocationAttribute> LocationAttribute { get; set; }
        public virtual DbSet<X_Log> X_Log { get; set; }
        public virtual DbSet<PrecautionaryStatement> PrecautionaryStatement { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermissions> RolePermissions { get; set; }
        public virtual DbSet<StatementPictogram> StatementPictogram { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<OverridePermissions> OverridePermissions { get; set; }





        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Chemical> Chemical { get; set; }
        // public virtual DbSet<ChemicalHazards> ChemicalHazards { get; set; }
        public virtual DbSet<Container> Container { get; set; }
        // public virtual DbSet<Hazard> Hazard { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<X_Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress; Initial Catalog=chemstore;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //ContainerChemicals (composite keys)
            modelBuilder.Entity<ContainerChemicals>()
                .HasKey(a => new { a.ContainerID, a.ChemicalCAS });

            //HazardPrecaution (composite keys)
            modelBuilder.Entity<HazardPrecaution>()
                .HasKey(a => new { a.HCode, a.PCode });

            //LocationAttribute (composite keys)
            modelBuilder.Entity<LocationAttribute>()
                .HasKey(a => new { a.LocationID, a.Key });

            //RolePermissions (composite keys)
            modelBuilder.Entity<RolePermissions>()
                .HasKey(a => new { a.RoleID, a.LocationID, a.Permission });

            //StatementPictogram (composite keys)
            modelBuilder.Entity<StatementPictogram>()
                .HasKey(a => new { a.GHCode, a.HCode });

            //OverridePermissions (composite keys)
            modelBuilder.Entity<OverridePermissions>()
                .HasKey(a => new { a.UserID, a.LocationID, a.Permission });



            OnModelCreatingPartial(modelBuilder);

            /*
            modelBuilder.Entity<Hazard>().HasData(
                   new Hazard { HazardId = "Corrosion", Description = "Corrosive" },
                   new Hazard { HazardId = "Environment", Description = "Enviornmental Hazard" },
                   new Hazard { HazardId = "ExclamationMark", Description = "Exclamation Mark" },
                   new Hazard { HazardId = "ExplodingBomb", Description = "Exploding Bomb" },
                   new Hazard { HazardId = "FlameOverCircle", Description = "Flame Over Circle" },
                   new Hazard { HazardId = "Flame", Description = "Flame" },
                   new Hazard { HazardId = "GasCylinder", Description = "Gas Cylinder" },
                   new Hazard { HazardId = "HealthHazard", Description = "HealthHazard" },
                   new Hazard { HazardId = "Skull", Description = "Skull" });
            */
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public void LogChanges()
        {
            var changeList = from e in ChangeTracker.Entries()
                             where e.State != EntityState.Detached && e.State != EntityState.Unchanged
                             && (e.Entity is Container || e.Entity is Account || e.Entity is Chemical)
                             select e;

            List<X_Log> logList = new List<X_Log>();

            string uname = httpContext.HttpContext.User.Identity.Name;
            int userId = (from a in Account
                          where a.Email == uname
                          select a.AccountId).FirstOrDefault();

            foreach (var entity in changeList)
            {
                //old log converted to X_Log
                X_Log newLog = new X_Log
                {
                    //Datetime converted to Timestamp
                    Timestamp = DateTime.Now,
                    UserID = userId
                };
                //Container changes
                if (entity.Entity is Container container)
                {
                    newLog.Table = "container";
                    //Assumed Key1 to reference container TODO: review and see if log needs container information
                    newLog.Key1 = container.ContainerId.ToString();
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
                                    logList.Add(new X_Log
                                    {
                                        Timestamp = DateTime.Now,
                                        Key1 = container.ContainerId.ToString(),
                                        Table = "container",
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
                else if (entity.Entity is Chemical chemical)
                {
                    //newLog.ChemicalCAS = chemical.CasNumber;
                    switch (entity.State)
                    {
                        case EntityState.Deleted:
                            newLog.Action = ((int)Actions.ChemicalRemoved);
                            break;

                        case EntityState.Added:
                            newLog.Action = ((int)Actions.ChemicalAdded);
                            break;

                        case EntityState.Modified:
                            newLog.Action = ((int)Actions.ChemicalEdited);
                            break;
                    }
                }
                else if (entity.Entity is Account account)
                //Account changes. This will need to be modified if we modify the logic
                {
                    newLog.Key1 = account.AccountId.ToString();
                    newLog.Table = "account";
                    //if they are added and they aren't just a member, or their new role is less their old role, they have been promoted
                    if ((entity.State == EntityState.Added && ((int)entity.CurrentValues["Role"]) != ((int)Roles.Member)) ||
                        (entity.State == EntityState.Modified && (((int)entity.CurrentValues["Role"]) < ((int)entity.OriginalValues["Role"]))))
                    {
                        newLog.Action = ((int)Actions.UserPromoted);
                        newLog.Notes = $"Promoted User {entity.CurrentValues["Name"]} - {entity.CurrentValues["Email"]} to {EnumHelper.GetDisplayValue((Roles)entity.CurrentValues["Role"])}";
                    }
                    else if (entity.State == EntityState.Deleted ||
                      (entity.State == EntityState.Modified && (((int)entity.CurrentValues["Role"]) > ((int)entity.OriginalValues["Role"]))))
                    {
                        newLog.Action = ((int)Actions.UserDemoted);
                        newLog.Notes = $"Demoted User {entity.CurrentValues["Name"]} - {entity.CurrentValues["Email"]} to {(entity.State == EntityState.Deleted ? "Member" : EnumHelper.GetDisplayValue((Roles)entity.CurrentValues["Role"]))}";
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
