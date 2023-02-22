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
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress; Initial Catalog=chemstore;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

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
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

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
