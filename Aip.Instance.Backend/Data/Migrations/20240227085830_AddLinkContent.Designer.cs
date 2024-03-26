﻿// <auto-generated />
using System;
using Aip.Instance.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aip.Instance.Backend.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240227085830_AddLinkContent")]
    partial class AddLinkContent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.FileContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileId")
                        .HasColumnType("text");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("InternshipId");

                    b.HasIndex("SectionId");

                    b.ToTable("FileContents");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.Flow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.Internship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Internships");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.InternshipUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "InternshipId");

                    b.HasIndex("InternshipId");

                    b.HasIndex("RoleId");

                    b.ToTable("InternshipBasedRoles");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.LinkContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InternshipId");

                    b.HasIndex("SectionId");

                    b.ToTable("LinkContents");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9a6bcced-f1d3-4155-b691-480718ee78e4"),
                            Name = "PrimaryTutor"
                        },
                        new
                        {
                            Id = new Guid("c82073b9-126c-4b4e-8edc-bc3d0cea56f1"),
                            Name = "InvitedTutor"
                        },
                        new
                        {
                            Id = new Guid("c0aa6455-626c-49de-94fc-616b4a6b5605"),
                            Name = "Intern"
                        });
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Id");

                    b.ToTable("Sections");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a19c6184-46c3-40fa-bef7-32ab966a4946"),
                            Name = "Лекции"
                        },
                        new
                        {
                            Id = new Guid("965d07a0-8666-49a9-af0e-797944c65f5f"),
                            Name = "Лабораторные работы"
                        },
                        new
                        {
                            Id = new Guid("adb191be-1e90-46e2-9886-1bfe911a0d66"),
                            Name = "Самостоятельная работа студентов"
                        },
                        new
                        {
                            Id = new Guid("d2604c2a-550a-4f7e-8da8-1864e6337377"),
                            Name = "Экзамен"
                        },
                        new
                        {
                            Id = new Guid("d10a8149-afb3-416b-8bfe-3dbbedbec517"),
                            Name = "Курсовой проект"
                        },
                        new
                        {
                            Id = new Guid("fa013e01-8a58-4ba5-b426-72b790c3ba4f"),
                            Name = "Курсовая работа"
                        },
                        new
                        {
                            Id = new Guid("33fe1815-0e3b-4991-8add-c163a2286711"),
                            Name = "Зачёт"
                        });
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.StaticFile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Checksum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Filepath")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StaticFiles");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.TextContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("InternshipId");

                    b.HasIndex("SectionId");

                    b.ToTable("TextContents");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<Guid?>("FlowId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("FlowId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlowInternship", b =>
                {
                    b.Property<Guid>("AssignedFlowsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InternshipsId")
                        .HasColumnType("uuid");

                    b.HasKey("AssignedFlowsId", "InternshipsId");

                    b.HasIndex("InternshipsId");

                    b.ToTable("FlowInternship");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.FileContent", b =>
                {
                    b.HasOne("Aip.Instance.Backend.Data.Models.StaticFile", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("Aip.Instance.Backend.Data.Models.Internship", "Internship")
                        .WithMany()
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aip.Instance.Backend.Data.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Internship");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.InternshipUserRole", b =>
                {
                    b.HasOne("Aip.Instance.Backend.Data.Models.Internship", "Internship")
                        .WithMany("InternshipUserRoles")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aip.Instance.Backend.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aip.Instance.Backend.Data.Models.User", "User")
                        .WithMany("IntershipUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Internship");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.LinkContent", b =>
                {
                    b.HasOne("Aip.Instance.Backend.Data.Models.Internship", "Internship")
                        .WithMany()
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aip.Instance.Backend.Data.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Internship");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.TextContent", b =>
                {
                    b.HasOne("Aip.Instance.Backend.Data.Models.Internship", "Internship")
                        .WithMany()
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aip.Instance.Backend.Data.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Internship");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.User", b =>
                {
                    b.HasOne("Aip.Instance.Backend.Data.Models.Flow", null)
                        .WithMany("Students")
                        .HasForeignKey("FlowId");
                });

            modelBuilder.Entity("FlowInternship", b =>
                {
                    b.HasOne("Aip.Instance.Backend.Data.Models.Flow", null)
                        .WithMany()
                        .HasForeignKey("AssignedFlowsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aip.Instance.Backend.Data.Models.Internship", null)
                        .WithMany()
                        .HasForeignKey("InternshipsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.Flow", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.Internship", b =>
                {
                    b.Navigation("InternshipUserRoles");
                });

            modelBuilder.Entity("Aip.Instance.Backend.Data.Models.User", b =>
                {
                    b.Navigation("IntershipUserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
