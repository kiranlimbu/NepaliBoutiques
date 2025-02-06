var builder = WebApplication.CreateBuilder(args);

// Add configuration sources
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()  // Add environment variables
    .AddUserSecrets<Program>(); // Add user secrets in development

// Add minimal services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

// Minimal middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
