using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }
        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(Configuration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
                .HasMany(ce => ce.CourseEnrollments)
                .WithOne(s => s.Student)
                .HasForeignKey(fk => fk.CourseId);

            builder.Entity<Student>()
            .HasMany(hs => hs.HomeworkSubmissions)
            .WithOne(s => s.Student)
            .HasForeignKey(fk => fk.StudentId);

            builder.Entity<Course>()
            .HasMany(se => se.StudentsEnrolled)
            .WithOne(s => s.Course)
            .HasForeignKey(fk => fk.CourseId);

            builder.Entity<Course>()
            .HasMany(se => se.Resources)
            .WithOne(s => s.Course)
            .HasForeignKey(fk => fk.CourseId);

            builder.Entity<Course>()
            .HasMany(se => se.HomeworkSubmissions)
            .WithOne(s => s.Course)
            .HasForeignKey(fk => fk.CourseId);

            builder.Entity<StudentCourse>()
            .HasKey(k => new { k.CourseId, k.StudentId });

            builder.Entity<StudentCourse>(e =>
            {
                e.Property(x => x.CourseId).IsRequired(true);
                e.Property(x => x.StudentId).IsRequired(true);
            });


            builder.Entity<Student>(e =>
            {
                e.Property(n => n.StudentId).IsRequired(true);
                e.Property(n => n.Name).HasColumnType("nvarchar(100)").IsRequired(true);
                e.Property(n => n.PhoneNumber).HasColumnType("char(10)");
                e.Property(n => n.RegisteredOn).IsRequired(true);
                e.Property(n => n.Birthday).IsRequired(false);
            });

            builder.Entity<Course>(e =>
            {
                e.Property(n => n.CourseId).IsRequired(true);
                e.Property(n => n.Name).HasColumnType("nvarchar(80)")
                .IsRequired(true);
                e.Property(n => n.Description).HasColumnType("ntext");
                e.Property(n => n.StartDate).IsRequired(true);
                e.Property(n => n.EndDate).IsRequired(true);
                e.Property(n => n.Price).IsRequired(true);
            });

            builder.Entity<Resource>(e =>
            {
                e.Property(n => n.ResourceId).IsRequired(true);
                e.Property(n => n.Name).HasColumnType("nvarchar(50)")
                .IsRequired(true);
                e.Property(n => n.Url).HasColumnType("varchar(max)");
                e.Property(n => n.ResourceType).IsRequired(true);
                e.Property(n => n.CourseId).IsRequired(true);
            });

            builder.Entity<Homework>(e =>
            {
                e.Property(n => n.HomeworkId).IsRequired(true);
                e.Property(n => n.Content).HasColumnType("varchar(max)").IsRequired(true);
                e.Property(n => n.ContentType).IsRequired(true);
                e.Property(n => n.SubmissionTime).IsRequired(true);
                e.Property(n => n.StudentId).IsRequired(true);
                e.Property(n => n.CourseId).IsRequired(true);
            });
        }
    }
}
