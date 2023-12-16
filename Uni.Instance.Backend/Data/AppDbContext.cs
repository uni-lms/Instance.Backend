using Microsoft.EntityFrameworkCore;

using Uni.Instance.Backend.Data.Models;


namespace Uni.Instance.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
  public virtual DbSet<User> Users => Set<User>();

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<User>().HasIndex(e => e.Email);
  }
}