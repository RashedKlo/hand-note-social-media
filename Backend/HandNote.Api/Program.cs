using HandNote.Api.Extensions;
using HandNote.Data.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Configure Microsoft ILogger with high performance
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

if (builder.Environment.IsProduction())
{
    builder.Logging.SetMinimumLevel(LogLevel.Warning);
}
else
{
    builder.Logging.SetMinimumLevel(LogLevel.Information);
}

// Add file logging if needed (requires NLog or similar)
// builder.Logging.AddFile("logs/handnote-{Date}.txt");

// Add controllers with optimized settings
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

//  core services
builder.Services.AddServices();

// High-performance configurations
builder.Services.AddPerformance();
builder.Services.AddHighPerformanceJson();

// Authentication and authorization
builder.Services.AddAuthentication(builder.Configuration);

// API documentation
builder.Services.AddSwagger();

// CORS configuration
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
    ?? new[] { "http://localhost:5173", "https://localhost:5173" };
builder.Services.AddCors(allowedOrigins);

// Validation
builder.Services.AddValidation();

var app = builder.Build();

// Configure pipeline for high performance
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HandNote API v1");
        c.RoutePrefix = "swagger";
        c.DisplayRequestDuration();
    });
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

// Performance middleware (order matters)
app.UseResponseCompression();
app.UseHttpsRedirection();

// CORS before authentication
app.UseCors("CorsPolicy");

// Security middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();


// Get logger and log startup
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("HandNote API with  architecture starting...");

try
{
    app.Run();
    logger.LogInformation("HandNote API started successfully");
}
catch (Exception ex)
{
    logger.LogCritical(ex, "HandNote API failed to start");
    throw;
}