using LMS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistence;

public class LMSDbContext(DbContextOptions<LMSDbContext> options) : DbContext(options)
{
    const string Schema = "lms";

    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set default schema for the entire context
        modelBuilder.HasDefaultSchema("lms");
    }
}
