﻿// <auto-generated />
using System;
using Uni.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230723154519_AllowMultipleOwnersOnCourse")]
    partial class AllowMultipleOwnersOnCourse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Courses.Contract.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Semester")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Groups.Contract.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Roles.Contract.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c80ea5a3-0687-4d8e-b23c-3f9352d1ad05"),
                            Name = "Student"
                        },
                        new
                        {
                            Id = new Guid("0330d24d-1501-4cc5-bd6a-fbfe31c2a6f4"),
                            Name = "Tutor"
                        },
                        new
                        {
                            Id = new Guid("a6c4a4bd-e4b5-44a0-a2dd-b3e9b42bc240"),
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Users.Contract.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Groups.Contract.Group", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.Courses.Contract.Course", null)
                        .WithMany("AssignedGroups")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Users.Contract.User", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.Courses.Contract.Course", null)
                        .WithMany("Owners")
                        .HasForeignKey("CourseId");

                    b.HasOne("Uni.Instance.Backend.Modules.Groups.Contract.Group", null)
                        .WithMany("Students")
                        .HasForeignKey("GroupId");

                    b.HasOne("Uni.Instance.Backend.Modules.Roles.Contract.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Courses.Contract.Course", b =>
                {
                    b.Navigation("AssignedGroups");

                    b.Navigation("Owners");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Groups.Contract.Group", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
