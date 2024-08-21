using LMS.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureServices();

WebApplication app = builder.Build();

app.ConfigurePipeline();

app.Run();
