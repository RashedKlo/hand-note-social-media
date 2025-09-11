using HandNote.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add custom services
builder.Services.AddApplicationServices();
builder.Services.AddAuthenticationSchemes(builder.Configuration);
builder.Services.AddSwaggerWithJwt();
builder.Services.AddCustomValidationResponse();

// Add CORS policy
string[] allowedOrigins = { "http://localhost:5173", "https://localhost:5173" };
builder.Services.AddCorsPolicy("AllowSpecificOrigins", allowedOrigins);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HandNote API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Use CORS (before authentication)
app.UseAppCors("AllowSpecificOrigins");

// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();