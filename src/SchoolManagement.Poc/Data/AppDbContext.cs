using Microsoft.EntityFrameworkCore;
using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.Student)
            .WithOne(s => s.Address)
            .HasForeignKey<Student>(s => s.AddressId);
    }
}