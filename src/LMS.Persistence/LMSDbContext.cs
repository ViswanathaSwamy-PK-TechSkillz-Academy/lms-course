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
        modelBuilder.HasDefaultSchema(Schema);

        // Ensure this is correctly configured
        modelBuilder.Entity<Microsoft.EntityFrameworkCore.Migrations.HistoryRow>().HasKey(e => e.MigrationId);

        // Customize the schema for the __EFMigrationsHistory table. Stores migration history in "lms" schema
        modelBuilder.Entity<Microsoft.EntityFrameworkCore.Migrations.HistoryRow>()
            .ToTable("__EFMigrationsHistory", Schema);
    }
}
