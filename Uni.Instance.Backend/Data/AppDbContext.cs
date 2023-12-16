using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Data.Models;


namespace Uni.Instance.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
  public virtual DbSet<User> Users => Set<User>();
  public virtual DbSet<Role> Roles => Set<Role>();

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<User>().HasIndex(e => e.Email);

    modelBuilder.Entity<Role>().HasData(new List<Role>() {
      new() { Id = Guid.Parse("9a6bcced-f1d3-4155-b691-480718ee78e4"), Name = "Admin" },
      new() { Id = Guid.Parse("c82073b9-126c-4b4e-8edc-bc3d0cea56f1"), Name = "Tutor" },
      new() { Id = Guid.Parse("c0aa6455-626c-49de-94fc-616b4a6b5605"), Name = "Student" },
    });
  }
}