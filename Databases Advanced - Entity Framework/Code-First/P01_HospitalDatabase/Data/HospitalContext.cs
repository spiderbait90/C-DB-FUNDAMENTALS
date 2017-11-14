using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
                builder.UseSqlServer(Configuration.ConectionsString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Patient>(a =>
            {
                a.Property(b => b.FirstName).HasColumnType("nvarchar(50)");
                a.Property(b => b.LastName).HasColumnType("nvarchar(50)");
                a.Property(b => b.Address).HasColumnType("nvarchar(250)");
                a.Property(b => b.Email).HasColumnType("varchar(80)");
            });

            builder.Entity<Patient>()
                .HasMany(v => v.Visitations)
                .WithOne(p => p.Patient)
                .HasForeignKey(f => f.PatientId);

            builder.Entity<Patient>()
               .HasMany(v => v.Diagnoses)
               .WithOne(p => p.Patient)
               .HasForeignKey(f => f.PatientId);

            builder.Entity<Visitation>(a =>
            {
                a.Property(b => b.Comments).HasColumnType("nvarchar(250)");
            });

            builder.Entity<Diagnose>(a =>
            {
                a.Property(b => b.Name).HasColumnType("nvarchar(50)");
                a.Property(b => b.Comments).HasColumnType("nvarchar(250)");
            });

            builder.Entity<Medicament>(a =>
            {
                a.Property(b => b.Name).HasColumnType("nvarchar(50)");
            });

            builder.Entity<PatientMedicament>()
                .ToTable("PatientsMedicaments")
                .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

            builder.Entity<Doctor>(a =>
            {
                a.Property(b => b.Name).HasColumnType("nvarchar(100)");
                a.Property(b => b.Specialty).HasColumnType("nvarchar(100)");
            });

            builder.Entity<Doctor>()
                .HasMany(v => v.Visitations)
                .WithOne(d => d.Doctor)
                .HasForeignKey(f => f.DoctorId);
        }

    }
}
