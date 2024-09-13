using LMS.IdentityPersistence;
using LMS.Persistence;
using LMS.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LMS.Web.Extensions;

public static class ServiceConfigurationExtensions
{
    public static void AddConfiguredServices(this WebApplicationBuilder builder)
    {
        string lmsIdentityDbConnectionString = builder.Configuration.GetConnectionString("LMSIdentityDbConnection") ?? throw new InvalidOperationException("Connection string 'LMSIdentityDbConnection' not found.");
        _ = builder.Services.AddDbContext<LMSIdentityDbContext>(options => options.UseSqlServer(lmsIdentityDbConnectionString));

        string lmsDbConnectionString = builder.Configuration.GetConnectionString("LMSDbConnection") ?? throw new InvalidOperationException("Connection string 'LMSDbConnection' not found.");
        _ = builder.Services.AddDbContext<LMSDbContext>(options => options.UseSqlServer(lmsDbConnectionString));

        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        _ = builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        _ = builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();

        _ = builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<LMSIdentityDbContext>();

        _ = builder.Services.AddControllersWithViews();
    }

}
