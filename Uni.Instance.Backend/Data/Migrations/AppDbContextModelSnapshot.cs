﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Uni.Backend.Data;

#nullable disable

namespace Uni.Backend.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseCourseBlock", b =>
                {
                    b.Property<Guid>("BlocksId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid");

                    b.HasKey("BlocksId", "CoursesId");

                    b.HasIndex("CoursesId");

                    b.ToTable("CourseCourseBlock");
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

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.Property<Guid>("OwnedCoursesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OwnersId")
                        .HasColumnType("uuid");

                    b.HasKey("OwnedCoursesId", "OwnersId");

                    b.HasIndex("OwnersId");

                    b.ToTable("CourseUser");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeamsId")
                        .HasColumnType("uuid");

                    b.HasKey("MembersId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("Uni.Backend.Modules.AssignmentSolutions.Contracts.AssignmentSolution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TeamId");

                    b.ToTable("AssignmentSolutions");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Assignments.Contracts.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AvailableUntil")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("BlockId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.Property<int>("MaximumPoints")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("CourseId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseBlocks.Contracts.CourseBlock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.File.Contracts.FileContent", b =>
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

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("CourseId");

                    b.HasIndex("FileId");

                    b.ToTable("FileContents");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Quiz.Contracts.AccruedPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AmountOfPoints")
                        .HasColumnType("integer");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("QuizPassAttemptId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuizPassAttemptId");

                    b.ToTable("AccruedPoints");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Quiz.Contracts.QuestionChoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AmountOfPoints")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("MultipleChoiceQuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MultipleChoiceQuestionId");

                    b.ToTable("QuestionChoices");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Quiz.Contracts.QuizPassAttempt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.HasIndex("UserId");

                    b.ToTable("QuizPassAttempts");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Text.Contract.TextContent", b =>
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

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("ContentId");

                    b.HasIndex("CourseId");

                    b.ToTable("TextContents");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Courses.Contracts.Course", b =>
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

            modelBuilder.Entity("Uni.Backend.Modules.Genders.Contracts.Gender", b =>
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

            modelBuilder.Entity("Uni.Backend.Modules.Groups.Contracts.Group", b =>
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

            modelBuilder.Entity("Uni.Backend.Modules.Roles.Contracts.Role", b =>
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

            modelBuilder.Entity("Uni.Backend.Modules.SolutionChecks.Contracts.SolutionCheck", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CheckedById")
                        .HasColumnType("uuid");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<Guid>("SolutionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CheckedById");

                    b.HasIndex("SolutionId");

                    b.ToTable("SolutionChecks");
                });

            modelBuilder.Entity("Uni.Backend.Modules.SolutionComments.Contracts.SolutionComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("SolutionCheckId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("WasEdited")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("SolutionCheckId");

                    b.ToTable("SolutionComments");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Static.Contracts.StaticFile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<Guid?>("AssignmentSolutionId")
                        .HasColumnType("uuid");

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

                    b.HasIndex("AssignmentSolutionId");

                    b.ToTable("StaticFiles");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Teams.Contracts.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Users.Contracts.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarId")
                        .HasColumnType("text");

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

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("GenderId");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.MultipleChoiceQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsGivingPointsForIncompleteAnswersEnabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMultipleChoicesAllowed")
                        .HasColumnType("boolean");

                    b.Property<int>("MaximumPoints")
                        .HasColumnType("integer");

                    b.Property<Guid?>("QuizContentId")
                        .HasColumnType("uuid");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuizContentId");

                    b.ToTable("MultipleChoiceQuestions");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.QuizContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AmountOfAllowedAttempts")
                        .HasColumnType("integer");

                    b.Property<DateTime>("AvailableUntil")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CourseBlockId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsQuestionsShuffled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVisibleToStudents")
                        .HasColumnType("boolean");

                    b.Property<TimeSpan?>("TimeLimit")
                        .HasColumnType("interval");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseBlockId");

                    b.HasIndex("CourseId");

                    b.ToTable("QuizContents");
                });

            modelBuilder.Entity("CourseCourseBlock", b =>
                {
                    b.HasOne("Uni.Backend.Modules.CourseBlocks.Contracts.CourseBlock", null)
                        .WithMany()
                        .HasForeignKey("BlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseGroup", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Groups.Contracts.Group", null)
                        .WithMany()
                        .HasForeignKey("AssignedGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseUser", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", null)
                        .WithMany()
                        .HasForeignKey("OwnedCoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Users.Contracts.User", null)
                        .WithMany()
                        .HasForeignKey("OwnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Users.Contracts.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Teams.Contracts.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Uni.Backend.Modules.AssignmentSolutions.Contracts.AssignmentSolution", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Assignments.Contracts.Assignment", "Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Users.Contracts.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Uni.Backend.Modules.Teams.Contracts.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Assignment");

                    b.Navigation("Author");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Assignments.Contracts.Assignment", b =>
                {
                    b.HasOne("Uni.Backend.Modules.CourseBlocks.Contracts.CourseBlock", "Block")
                        .WithMany()
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.File.Contracts.FileContent", b =>
                {
                    b.HasOne("Uni.Backend.Modules.CourseBlocks.Contracts.CourseBlock", "Block")
                        .WithMany()
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", "Course")
                        .WithMany("FileContents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Static.Contracts.StaticFile", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.Navigation("Block");

                    b.Navigation("Course");

                    b.Navigation("File");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Quiz.Contracts.AccruedPoint", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.MultipleChoiceQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.CourseContents.Quiz.Contracts.QuizPassAttempt", null)
                        .WithMany("AccruedPoints")
                        .HasForeignKey("QuizPassAttemptId");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Quiz.Contracts.QuestionChoice", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.MultipleChoiceQuestion", null)
                        .WithMany("Choices")
                        .HasForeignKey("MultipleChoiceQuestionId");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Quiz.Contracts.QuizPassAttempt", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.QuizContent", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Users.Contracts.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Text.Contract.TextContent", b =>
                {
                    b.HasOne("Uni.Backend.Modules.CourseBlocks.Contracts.CourseBlock", "Block")
                        .WithMany()
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Static.Contracts.StaticFile", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", "Course")
                        .WithMany("TextContents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");

                    b.Navigation("Content");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Uni.Backend.Modules.SolutionChecks.Contracts.SolutionCheck", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Users.Contracts.User", "CheckedBy")
                        .WithMany()
                        .HasForeignKey("CheckedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.AssignmentSolutions.Contracts.AssignmentSolution", "Solution")
                        .WithMany("Checks")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckedBy");

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("Uni.Backend.Modules.SolutionComments.Contracts.SolutionComment", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Users.Contracts.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.SolutionChecks.Contracts.SolutionCheck", null)
                        .WithMany("Comments")
                        .HasForeignKey("SolutionCheckId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Static.Contracts.StaticFile", b =>
                {
                    b.HasOne("Uni.Backend.Modules.AssignmentSolutions.Contracts.AssignmentSolution", null)
                        .WithMany("Files")
                        .HasForeignKey("AssignmentSolutionId");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Teams.Contracts.Team", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Users.Contracts.User", b =>
                {
                    b.HasOne("Uni.Backend.Modules.Static.Contracts.StaticFile", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.HasOne("Uni.Backend.Modules.Genders.Contracts.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("Uni.Backend.Modules.Groups.Contracts.Group", null)
                        .WithMany("Students")
                        .HasForeignKey("GroupId");

                    b.HasOne("Uni.Backend.Modules.Roles.Contracts.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Avatar");

                    b.Navigation("Gender");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.MultipleChoiceQuestion", b =>
                {
                    b.HasOne("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.QuizContent", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuizContentId");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.QuizContent", b =>
                {
                    b.HasOne("Uni.Backend.Modules.CourseBlocks.Contracts.CourseBlock", "CourseBlock")
                        .WithMany()
                        .HasForeignKey("CourseBlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Backend.Modules.Courses.Contracts.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("CourseBlock");
                });

            modelBuilder.Entity("Uni.Backend.Modules.AssignmentSolutions.Contracts.AssignmentSolution", b =>
                {
                    b.Navigation("Checks");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("Uni.Backend.Modules.CourseContents.Quiz.Contracts.QuizPassAttempt", b =>
                {
                    b.Navigation("AccruedPoints");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Courses.Contracts.Course", b =>
                {
                    b.Navigation("FileContents");

                    b.Navigation("TextContents");
                });

            modelBuilder.Entity("Uni.Backend.Modules.Groups.Contracts.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Uni.Backend.Modules.SolutionChecks.Contracts.SolutionCheck", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.MultipleChoiceQuestion", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts.QuizContent", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
