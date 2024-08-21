using LMS.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        _ = builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        _ = builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();

        _ = builder.Services.AddControllersWithViews();
    }

}
