﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentManagementSystem.Data;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    [Migration("20230524131232_intial")]
    partial class intial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("designation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("admins");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FacultyDetailsId")
                        .HasColumnType("int")
                        .HasColumnName("FacultyId");

                    b.Property<int>("Fees")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("FacultyDetailsId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.EnrollDetails", b =>
                {
                    b.Property<int>("EnrollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("FacultyDetailsFacultyId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("EnrollId");

                    b.HasIndex("CourseId");

                    b.HasIndex("FacultyDetailsFacultyId");

                    b.HasIndex("StudentId");

                    b.ToTable("EnrollDetails");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.FacultyDetails", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("designation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("phoneNo")
                        .HasColumnType("bigint");

                    b.HasKey("FacultyId");

                    b.ToTable("facultyDetails");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.Student", b =>
                {
                    b.Property<int>("RollNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("PhoneNo")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RollNo");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.Course", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Domain.FacultyDetails", "FacultyDetails")
                        .WithMany()
                        .HasForeignKey("FacultyDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FacultyDetails");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.EnrollDetails", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Domain.Course", "course")
                        .WithMany("Enroll")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentManagementSystem.Models.Domain.FacultyDetails", null)
                        .WithMany("Enroll")
                        .HasForeignKey("FacultyDetailsFacultyId");

                    b.HasOne("StudentManagementSystem.Models.Domain.Student", "student")
                        .WithMany("Enroll")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("student");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.Course", b =>
                {
                    b.Navigation("Enroll");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.FacultyDetails", b =>
                {
                    b.Navigation("Enroll");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Domain.Student", b =>
                {
                    b.Navigation("Enroll");
                });
#pragma warning restore 612, 618
        }
    }
}