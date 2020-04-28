using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SHS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser, ApplicationIdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationIdentityUser>(b =>
            {
                b.ToTable("IdentityUser");
            });
            builder.Entity<ApplicationIdentityRole>(b =>
            {
                b.ToTable("IdentityRole");
            });
            builder.Entity<ApplicationIdentityUser>().HasOne(l => l.Teacher).WithOne(l => l.IdentityUser).HasForeignKey<ApplicationIdentityUser>(l => l.TeacherId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Teacher>(entity =>
            {
                entity.HasOne(l => l.Course)
                    .WithMany(l => l.Teachers)
                    .HasForeignKey(l => l.CourseId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(l => l.College)
                    .WithMany(l => l.Teachers)
                    .HasForeignKey(l => l.ColleageId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            builder.Entity<College>(entity =>
            {
                entity.HasKey(e => e.CollegeId).HasName("collegeId");

                entity.ToTable("tb_college");

                entity.Property(e => e.CollegeName)
                    .HasColumnName("collegeName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnglishName)
                    .HasColumnName("englishName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            builder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("tb_class");

                entity.Property(e => e.ClassId)
                    .HasColumnName("classId");

                entity.Property(e => e.ClassName)
                    .HasColumnName("className")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(e => e.College)
                    .WithMany(e => e.Classes)
                    .HasForeignKey(e => e.CollegeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);
                entity.ToTable("tb_course");

                entity.Property(e => e.CourseId)
                    .HasColumnName("courseId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CourseName)
                    .HasColumnName("courseName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            builder.Entity<Student_Course>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });

                entity.ToTable("tb_student_course");

                entity.Property(e => e.StudentId)
                    .HasColumnName("studentId");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tb_SC_tb_course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tb_SC_tb_student");
            });

            builder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.ToTable("tb_student");

                entity.Property(e => e.StudentId)
                    .HasColumnName("studentId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday");

                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StudentName)
                    .HasColumnName("studentName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnglishName)
                    .HasColumnName("englishName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserFaceImgUrl)
                    .HasColumnName("userFaceImgUrl")
                    .IsUnicode(false);

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId).OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_tb_student_tb_class");
            });

            builder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.ToTable("tb_teacher");

                entity.Property(e => e.TeacherId)
                    .HasColumnName("teachenrId");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday");

                entity.Property(e => e.CourseId)
                    .HasColumnName("courseId")
                    .HasMaxLength(10)
                    .IsRequired(false);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasMaxLength(10);

                entity.Property(e => e.TeacherName)
                    .HasColumnName("teacherName")
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.EnglishName)
                    .HasColumnName("englishName")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student_Course> Sc { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
    }
}
