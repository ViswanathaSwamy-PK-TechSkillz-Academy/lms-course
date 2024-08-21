using LMS.IdentityPersistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Extensions;

public static class ServiceConfigurationExtensions
{
    public static void AddConfiguredServices(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("LMSIdentityDbConnection") ?? throw new InvalidOperationException("Connection string 'LMSIdentityDbConnection' not found.");

        _ = builder.Services.AddDbContext<LMSIdentityDbContext>(options => options.UseSqlServer(connectionString));

        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        _ = builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<LMSIdentityDbContext>();

        _ = builder.Services.AddControllersWithViews();
    }

}
