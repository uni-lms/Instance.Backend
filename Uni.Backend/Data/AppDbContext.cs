using Microsoft.EntityFrameworkCore;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Backend.Modules.CourseContents.Text.Contract;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Genders.Contracts;
using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Roles.Contracts;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Course> Courses => Set<Course>();
    public virtual DbSet<Group> Groups => Set<Group>();
    public virtual DbSet<Role> Roles => Set<Role>();
    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<StaticFile> StaticFiles => Set<StaticFile>();
    public virtual DbSet<Gender> Genders => Set<Gender>();
    public virtual DbSet<CourseBlock> CourseBlocks => Set<CourseBlock>();
    public virtual DbSet<TextContent> TextContents => Set<TextContent>();
    public virtual DbSet<FileContent> FileContents => Set<FileContent>();
    public virtual DbSet<QuizContent> QuizContents => Set<QuizContent>();
    public virtual DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions => Set<MultipleChoiceQuestion>();
    public virtual DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();
    public virtual DbSet<QuizPassAttempt> QuizPassAttempts => Set<QuizPassAttempt>();
    public virtual DbSet<AccruedPoint> AccruedPoints => Set<AccruedPoint>();

    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = new Guid("c80ea5a3-0687-4d8e-b23c-3f9352d1ad05"), Name = "Student" },
            new Role { Id = new Guid("0330d24d-1501-4cc5-bd6a-fbfe31c2a6f4"), Name = "Tutor" },
            new Role { Id = new Guid("a6c4a4bd-e4b5-44a0-a2dd-b3e9b42bc240"), Name = "Administrator" }
        );

        modelBuilder.Entity<Gender>().HasData(
            new Gender { Id = new Guid("8f10243b-6565-45bd-b459-e68ea8ef2536"), Name = "Male" },
            new Gender { Id = new Guid("f0b882f9-f4ed-4d23-abe4-378a4caefd72"), Name = "Female" }
        );

        modelBuilder.Entity<CourseBlock>().HasData(
            new CourseBlock { Id = new Guid("e6cb3460-fb09-43c1-84b4-3de4542470f5"), Name = "LabWorks" },
            new CourseBlock { Id = new Guid("90f395b7-aba3-4720-a338-d0260801c185"), Name = "Lectures" },
            new CourseBlock { Id = new Guid("a37250cc-47f0-4ac9-9069-9709db0c89e9"), Name = "CourseProject" },
            new CourseBlock { Id = new Guid("e777af32-eedb-4078-9fdd-2f4d3f489087"), Name = "FinalCertification" }
        );
    }
}