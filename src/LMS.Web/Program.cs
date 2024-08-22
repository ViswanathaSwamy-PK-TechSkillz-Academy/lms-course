using LMS.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddConfiguredServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseConfiguredPipeline();

app.Run();
