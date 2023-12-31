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
    [Migration("20230726100846_AddManyToManyBetweenCourseAndGroup")]
    partial class AddManyToManyBetweenCourseAndGroup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseBlocks.Contracts.CourseBlock", b =>
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

                    b.ToTable("CourseBlocks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e6cb3460-fb09-43c1-84b4-3de4542470f5"),
                            Name = "LabWorks"
                        },
                        new
                        {
                            Id = new Guid("90f395b7-aba3-4720-a338-d0260801c185"),
                            Name = "Lectures"
                        },
                        new
                        {
                            Id = new Guid("a37250cc-47f0-4ac9-9069-9709db0c89e9"),
                            Name = "CourseProject"
                        },
                        new
                        {
                            Id = new Guid("e777af32-eedb-4078-9fdd-2f4d3f489087"),
                            Name = "FinalCertification"
                        });
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.File.Contracts.FileContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BlockId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileId")
                        .HasColumnType("text");

                    b.Property<string>("IconId")
                        .HasColumnType("text");

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("CourseId");

                    b.HasIndex("FileId");

                    b.HasIndex("IconId");

                    b.ToTable("FileContents");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Text.Contract.TextContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BlockId")
                        .HasColumnType("uuid");

                    b.Property<string>("ContentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("IconId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("ContentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("IconId");

                    b.ToTable("TextContents");
                });

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

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Genders.Contracts.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8f10243b-6565-45bd-b459-e68ea8ef2536"),
                            Name = "Male"
                        },
                        new
                        {
                            Id = new Guid("f0b882f9-f4ed-4d23-abe4-378a4caefd72"),
                            Name = "Female"
                        });
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Groups.Contract.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CurrentSemester")
                        .HasColumnType("integer");

                    b.Property<int>("MaxSemester")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Static.Contracts.StaticFile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Checksum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VisibleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StaticFiles");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Users.Contract.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarId")
                        .HasColumnType("text");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("GenderId")
                        .HasColumnType("uuid");

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

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("CourseId");

                    b.HasIndex("GenderId");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseGroup", b =>
                {
                    b.Property<Guid>("AssignedGroupsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.HasKey("AssignedGroupsId", "CoursesId");

                    b.HasIndex("CoursesId");

                    b.ToTable("CourseGroup");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseBlocks.Contracts.CourseBlock", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.Courses.Contract.Course", null)
                        .WithMany("Blocks")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.File.Contracts.FileContent", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.CourseBlocks.Contracts.CourseBlock", "Block")
                        .WithMany()
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Instance.Backend.Modules.Courses.Contract.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Instance.Backend.Modules.Static.Contracts.StaticFile", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("Uni.Instance.Backend.Modules.Static.Contracts.StaticFile", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.Navigation("Block");

                    b.Navigation("Course");

                    b.Navigation("File");

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Text.Contract.TextContent", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.CourseBlocks.Contracts.CourseBlock", "Block")
                        .WithMany()
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Instance.Backend.Modules.Static.Contracts.StaticFile", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Instance.Backend.Modules.Courses.Contract.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Instance.Backend.Modules.Static.Contracts.StaticFile", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");

                    b.Navigation("Content");

                    b.Navigation("Course");

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Users.Contract.User", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.Static.Contracts.StaticFile", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.HasOne("Uni.Instance.Backend.Modules.Courses.Contract.Course", null)
                        .WithMany("Owners")
                        .HasForeignKey("CourseId");

                    b.HasOne("Uni.Instance.Backend.Modules.Genders.Contracts.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("Uni.Instance.Backend.Modules.Groups.Contract.Group", null)
                        .WithMany("Students")
                        .HasForeignKey("GroupId");

                    b.HasOne("Uni.Instance.Backend.Modules.Roles.Contract.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");

                    b.Navigation("Gender");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CourseGroup", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.Groups.Contract.Group", null)
                        .WithMany()
                        .HasForeignKey("AssignedGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Instance.Backend.Modules.Courses.Contract.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.Courses.Contract.Course", b =>
                {
                    b.Navigation("Blocks");

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
