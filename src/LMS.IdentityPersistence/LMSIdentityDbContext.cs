using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LMS.IdentityPersistence;

public class LMSIdentityDbContext(DbContextOptions<LMSIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seeding Default Data
        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        _ = modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "6d9ed3ff-bebb-42bc-ad07-0255bb0f7edb",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "cc4fcb01-de88-4c20-b4ac-8df5c2a65160",
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR"
            },
            new IdentityRole
            {
                Id = "e9f639de-624f-4a4e-b8bf-2381725462f1",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );

        PasswordHasher<IdentityUser> passwordHasher = new();

        ApplicationUser adminUser = new()
        {
            Id = "408aa945-3d84-4421-8342-7269ec64d949",
            Email = "admin@localhost.com",
            NormalizedEmail = "ADMIN@LOCALHOST.COM",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
            UserName = "admin@localhost.com",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            DateOfBirth = new DateOnly(1980, 1, 1)
        };

        // Hash the password for the admin user
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Sample@123$");

        _ = modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

        _ = modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e9f639de-624f-4a4e-b8bf-2381725462f1",
                    UserId = "408aa945-3d84-4421-8342-7269ec64d949"
                });
    }
}
