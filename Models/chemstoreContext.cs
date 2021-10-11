using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChemStoreWebApp.Models
{
    public partial class chemstoreContext : DbContext
    {
        public chemstoreContext()
        {
        }

        public chemstoreContext(DbContextOptions<chemstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Chemical> Chemical { get; set; }
        public virtual DbSet<ChemicalHazards> ChemicalHazards { get; set; }
        public virtual DbSet<Container> Container { get; set; }
        public virtual DbSet<Hazard> Hazard { get; set; }
        public virtual DbSet<Location> Location { get; set; }

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
    }
}
