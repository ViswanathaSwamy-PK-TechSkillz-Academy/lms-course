using LMS.Web.Extensions;
using LMS.IdentityPersistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LMSIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'LMSIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<LMSIdentityDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LMSIdentityDbContext>();

// Add services to the container.
builder.AddConfiguredServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseConfiguredPipeline();

app.Run();
