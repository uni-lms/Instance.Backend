using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Data.Models;


namespace Uni.Instance.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
  public virtual DbSet<User> Users => Set<User>();
  public virtual DbSet<Role> Roles => Set<Role>();
  public virtual DbSet<CourseSection> CourseSections => Set<CourseSection>();
  public virtual DbSet<Course> Courses => Set<Course>();
  public virtual DbSet<Group> Groups => Set<Group>();
  public virtual DbSet<CourseContentFile> FileContents => Set<CourseContentFile>();
  public virtual DbSet<StaticFile> StaticFiles => Set<StaticFile>();

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<User>().HasIndex(e => e.Email);

    modelBuilder.Entity<Role>().HasData(new List<Role>() {
      new() { Id = Guid.Parse("9a6bcced-f1d3-4155-b691-480718ee78e4"), Name = "Admin" },
      new() { Id = Guid.Parse("c82073b9-126c-4b4e-8edc-bc3d0cea56f1"), Name = "Tutor" },
      new() { Id = Guid.Parse("c0aa6455-626c-49de-94fc-616b4a6b5605"), Name = "Student" },
    });

    modelBuilder.Entity<CourseSection>().HasData(new List<CourseSection> {
      new() { Id = new Guid("a19c6184-46c3-40fa-bef7-32ab966a4946"), Name = "Лекции" },
      new() { Id = new Guid("965d07a0-8666-49a9-af0e-797944c65f5f"), Name = "Лабораторные работы" },
      new() { Id = new Guid("adb191be-1e90-46e2-9886-1bfe911a0d66"), Name = "Самостоятельная работа студентов" },
      new() { Id = new Guid("d2604c2a-550a-4f7e-8da8-1864e6337377"), Name = "Экзамен" },
      new() { Id = new Guid("d10a8149-afb3-416b-8bfe-3dbbedbec517"), Name = "Курсовой проект" },
      new() { Id = new Guid("fa013e01-8a58-4ba5-b426-72b790c3ba4f"), Name = "Курсовая работа" },
      new() { Id = new Guid("33fe1815-0e3b-4991-8add-c163a2286711"), Name = "Зачёт" },
    });

    modelBuilder.Entity<Group>().HasIndex(e => e.Name).IsUnique();
  }
}