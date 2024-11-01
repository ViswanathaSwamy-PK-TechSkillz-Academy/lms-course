using LMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LMS.Persistence;

public class LMSDbContext(DbContextOptions<LMSDbContext> options) : DbContext(options)
{
    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();

    public DbSet<LeaveAllocation> LeaveAllocations => Set<LeaveAllocation>();

    public DbSet<Period> Periods => Set<Period>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ignore the ApplicationUser entity in LMSDbContext
        modelBuilder.Ignore<ApplicationUser>();

        // Set default schema for the entire context
        modelBuilder.HasDefaultSchema("lms");

        // Configure the relationship
        modelBuilder.Entity<LeaveAllocation>()
            .HasOne(l => l.Employee)
            .WithMany()
            .HasForeignKey(l => l.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        // Seeding Default Data
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
