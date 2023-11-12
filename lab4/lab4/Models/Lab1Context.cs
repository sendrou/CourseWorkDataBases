using System;
using System.Collections.Generic;
using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab02;

public partial class Lab1Context : DbContext
{
    public Lab1Context()
    {
    }

    public Lab1Context(DbContextOptions<Lab1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBrand> CarBrands { get; set; }

    public virtual DbSet<CargoTransportation> CargoTransportations { get; set; }

    public virtual DbSet<Distance> Distances { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Load> Loads { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Settlement> Settlements { get; set; }

    public virtual DbSet<TransportationTariff> TransportationTariffs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0340E67C70070");

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.CarBrandId).HasColumnName("CarBrandID");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.CarBrand).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarBrandId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Cars__CarBrandID__4222D4EF");
        });

        modelBuilder.Entity<CarBrand>(entity =>
        {
            entity.HasKey(e => e.CarBrandId).HasName("PK__CarBrand__3EAE0B29712E2DC6");

            entity.Property(e => e.CarBrandId).HasColumnName("CarBrandID");
            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CargoTransportation>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__CargoTra__1ABEEF6F97EC5565");

            entity.ToTable("CargoTransportation");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DistanceId).HasColumnName("DistanceID");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.LoadId).HasColumnName("LoadID");
            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            entity.Property(e => e.TransportationTariffId).HasColumnName("TransportationTariffID");

            entity.HasOne(d => d.Car).WithMany(p => p.CargoTransportations)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CargoTran__CarID__4BAC3F29");

            entity.HasOne(d => d.Distance).WithMany(p => p.CargoTransportations)
                .HasForeignKey(d => d.DistanceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CargoTran__Dista__49C3F6B7");

            entity.HasOne(d => d.Driver).WithMany(p => p.CargoTransportations)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CargoTran__Drive__4AB81AF0");

            entity.HasOne(d => d.Load).WithMany(p => p.CargoTransportations)
                .HasForeignKey(d => d.LoadId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CargoTran__LoadI__4CA06362");

            entity.HasOne(d => d.Organization).WithMany(p => p.CargoTransportations)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CargoTran__Organ__48CFD27E");

            entity.HasOne(d => d.TransportationTariff).WithMany(p => p.CargoTransportations)
                .HasForeignKey(d => d.TransportationTariffId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CargoTran__Trans__4D94879B");
        });

        modelBuilder.Entity<Distance>(entity =>
        {
            entity.HasKey(e => e.DistanceId).HasName("PK__Distance__A24E2A1C2181A126");

            entity.Property(e => e.DistanceId).HasColumnName("DistanceID");
            entity.Property(e => e.ArrivalSettlementId).HasColumnName("ArrivalSettlementID");
            entity.Property(e => e.DeparturesSettlementId).HasColumnName("DeparturesSettlementID");
            entity.Property(e => e.Distance1).HasColumnName("Distance");

            entity.HasOne(d => d.ArrivalSettlement).WithMany(p => p.DistanceArrivalSettlements)
                .HasForeignKey(d => d.ArrivalSettlementId)
                .HasConstraintName("FK__Distances__Arriv__3B75D760");

            entity.HasOne(d => d.DeparturesSettlement).WithMany(p => p.DistanceDeparturesSettlements)
                .HasForeignKey(d => d.DeparturesSettlementId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Distances__Depar__3A81B327");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Drivers__F1B1CD24A6D77012");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PassportDetails)
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Load>(entity =>
        {
            entity.HasKey(e => e.LoadId).HasName("PK__Loads__4ED77A2DE7E6350B");

            entity.Property(e => e.LoadId).HasColumnName("LoadID");
            entity.Property(e => e.LoadName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B72E36729C0");

            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Settlement>(entity =>
        {
            entity.HasKey(e => e.SettlementId).HasName("PK__Settleme__771254BA6604459B");

            entity.Property(e => e.SettlementId).HasColumnName("SettlementID");
            entity.Property(e => e.SettlementName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransportationTariff>(entity =>
        {
            entity.HasKey(e => e.TransportationTariffId).HasName("PK__Transpor__A3368695B734D0C0");

            entity.Property(e => e.TransportationTariffId).HasColumnName("TransportationTariffID");
            entity.Property(e => e.TariffPerM3Km).HasColumnName("TariffPer(m^3*km)");
            entity.Property(e => e.TariffPerTKm).HasColumnName("TariffPer(t*km)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
