using Backend.Modules.Courses.Contract;
using Backend.Modules.Genders.Contracts;
using Backend.Modules.Roles.Contract;
using Backend.Modules.Users.Contract;
using Backend.Modules.Groups.Contract;
using Backend.Modules.Static.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Course> Courses => Set<Course>();
    public virtual DbSet<Group> Groups => Set<Group>();
    public virtual DbSet<Role> Roles => Set<Role>();
    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<StaticFile> StaticFiles => Set<StaticFile>();
    public virtual DbSet<Gender> Genders => Set<Gender>();

    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role {Id = new Guid("c80ea5a3-0687-4d8e-b23c-3f9352d1ad05"), Name = "Student"},
            new Role {Id = new Guid("0330d24d-1501-4cc5-bd6a-fbfe31c2a6f4"), Name = "Tutor"},
            new Role {Id = new Guid("a6c4a4bd-e4b5-44a0-a2dd-b3e9b42bc240"), Name = "Administrator"}
        );

        modelBuilder.Entity<Gender>().HasData(
            new Gender {Id = new Guid("8f10243b-6565-45bd-b459-e68ea8ef2536"), Name = "Male"},
            new Gender {Id = new Guid("f0b882f9-f4ed-4d23-abe4-378a4caefd72"), Name = "Female"}
        );
    }
}