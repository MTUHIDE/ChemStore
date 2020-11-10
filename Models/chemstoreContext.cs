using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ChemStoreWebApp.Models;

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

        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<ChemInContainer> ChemInContainer { get; set; }
        public virtual DbSet<Chemical> Chemical { get; set; }
        public virtual DbSet<Container> Container { get; set; }
        public virtual DbSet<HasHazard> HasHazard { get; set; }
        public virtual DbSet<HasLocation> HasLocation { get; set; }
        public virtual DbSet<Hazard> Hazard { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<PersonInCharge> PersonInCharge { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=127.0.0.1:3306; database=chemstore; user id=jasonhoffman; password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("building");

                entity.Property(e => e.BuildingId)
                    .HasColumnName("building_id")
                    .HasColumnType("int");

                entity.Property(e => e.BuildingName)
                    .HasColumnName("building_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChemInContainer>(entity =>
            {
                entity.ToTable("chem_in_container");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int");
            });

            modelBuilder.Entity<Chemical>(entity =>
            {
                entity.HasKey(e => e.CasNumber)
                    .HasName("PRIMARY_CAS");

                entity.ToTable("chemical");

                entity.Property(e => e.CasNumber)
                    .HasColumnName("cas_number")
                    .HasColumnType("int");

                entity.Property(e => e.ChemName)
                    .HasColumnName("chem_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.HazardId)
                    .HasColumnName("hazard_id")
                    .HasColumnType("int");
            });

            modelBuilder.Entity<Container>(entity =>
            {
                entity.ToTable("container");

                entity.Property(e => e.ContainerId)
                    .HasColumnName("container_id")
                    .HasColumnType("int");

                entity.Property(e => e.ChemId)
                    .HasColumnName("chem_id")
                    .HasColumnType("int");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasColumnType("int");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.ContainerNavigation)
                    .WithOne(p => p.Container)
                    .HasForeignKey<Container>(d => d.ContainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("container_id");
            });

            modelBuilder.Entity<HasHazard>(entity =>
            {
                entity.ToTable("has_hazard");

                entity.HasIndex(e => e.ChemicalId)
                    .HasName("chemical_id_idx");

                entity.HasIndex(e => e.HazardId)
                    .HasName("hazard_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int");

                entity.Property(e => e.ChemicalId)
                    .HasColumnName("chemical_id")
                    .HasColumnType("int");

                entity.Property(e => e.HazardId)
                    .HasColumnName("hazard_id")
                    .HasColumnType("int");

                entity.HasOne(d => d.Chemical)
                    .WithMany(p => p.HasHazard)
                    .HasForeignKey(d => d.ChemicalId)
                    .HasConstraintName("chemical_id");

                entity.HasOne(d => d.Hazard)
                    .WithMany(p => p.HasHazard)
                    .HasForeignKey(d => d.HazardId)
                    .HasConstraintName("hazard_id");
            });

            modelBuilder.Entity<HasLocation>(entity =>
            {
                entity.ToTable("has_location");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int");
            });

            modelBuilder.Entity<Hazard>(entity =>
            {
                entity.ToTable("hazard");

                entity.Property(e => e.HazardId)
                    .HasColumnName("hazard_id")
                    .HasColumnType("int");

                entity.Property(e => e.HazardDetails)
                    .HasColumnName("hazard_details")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasComment("Currently stores the location explicitly, not the foreign keys of each respective table.");

                entity.HasIndex(e => e.LocationFid)
                    .HasName("location_fid_idx");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasColumnType("int");

                entity.Property(e => e.Building)
                    .HasColumnName("building")
                    .HasColumnType("int");

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasColumnType("int");

                entity.Property(e => e.LocationFid)
                    .HasColumnName("location_fid")
                    .HasColumnType("int");

                entity.Property(e => e.Room)
                    .HasColumnName("room")
                    .HasColumnType("int");

                entity.HasOne(d => d.LocationF)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.LocationFid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_fid");
            });

            modelBuilder.Entity<PersonInCharge>(entity =>
            {
                entity.HasKey(e => e.PicId)
                    .HasName("PRIMARY_ID");

                entity.ToTable("person_in_charge");

                entity.Property(e => e.PicId)
                    .HasColumnName("pic_id")
                    .HasColumnType("int");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PicName)
                    .HasColumnName("pic_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pic)
                    .WithOne(p => p.PersonInCharge)
                    .HasForeignKey<PersonInCharge>(d => d.PicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pic_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<ChemStoreWebApp.Models.Role> Role { get; set; }
    }
}
