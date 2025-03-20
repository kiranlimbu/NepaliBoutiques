using API.Extensions;
using Application;
using Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add configuration sources
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()  // Add environment variables
    .AddUserSecrets<Program>(); // Add user secrets in development

// Add services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpClient();
// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // Angular dev server default port
            .AllowAnyMethod() // Allow all methods (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader() // Allow all headers
            .AllowCredentials(); // Allow credentials (cookies, authentication headers, etc.)
    });
});

var app = builder.Build();

// middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
    // add seed/mock data
    // app.AddSeedData(); // Run this only first time to populate the database with mock data
}

app.UseCustomExceptionHandler();

// app.UseAuthentication();
// app.UseAuthorization();

app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
