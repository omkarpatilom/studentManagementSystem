﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentExam.Data;

#nullable disable

namespace StudentExam.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20230901060858_intial mig")]
    partial class intialmig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StudentExam.Model.Domain.QuestionDetails", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CorrectAns")
                        .HasColumnType("longtext");

                    b.Property<string>("Option1")
                        .HasColumnType("longtext");

                    b.Property<string>("Option2")
                        .HasColumnType("longtext");

                    b.Property<string>("Option3")
                        .HasColumnType("longtext");

                    b.Property<string>("Option4")
                        .HasColumnType("longtext");

                    b.Property<string>("QuestionDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("StudentExam.Model.Domain.QuestionSet", b =>
                {
                    b.Property<int>("QuestionSetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<int?>("TestId")
                        .HasColumnType("int");

                    b.HasKey("QuestionSetId");

                    b.HasIndex("QuestionID");

                    b.HasIndex("TestId")
                        .IsUnique();

                    b.ToTable("QuestionSets");
                });

            modelBuilder.Entity("StudentExam.Model.Domain.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PassScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TestDesc")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TestTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TotalMarks")
                        .HasColumnType("int");

                    b.HasKey("TestId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("StudentExam.Model.Domain.QuestionSet", b =>
                {
                    b.HasOne("StudentExam.Model.Domain.QuestionDetails", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentExam.Model.Domain.Test", "Test")
                        .WithOne("QuestionSet")
                        .HasForeignKey("StudentExam.Model.Domain.QuestionSet", "TestId");

                    b.Navigation("Question");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("StudentExam.Model.Domain.Test", b =>
                {
                    b.Navigation("QuestionSet");
                });
#pragma warning restore 612, 618
        }
    }
}
