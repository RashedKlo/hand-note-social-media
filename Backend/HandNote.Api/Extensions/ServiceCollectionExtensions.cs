using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.User;
using HandNote.Data.Repositories.Conversation;
using HandNote.Data.Repositories.Message;
using HandNote.Data.Repositories.Notification;
using HandNote.Data.Repositories.Post;
using HandNote.Data.Repositories.PostReaction;
using HandNote.Data.Repositories.Media_Files;
using HandNote.Services.Interfaces;
using HandNote.Services;
using HandNote.Services.Conversation;
using HandNote.Services.MediaFiles;
using HandNote.Services.PostReaction;
using HandNote.Services.Message;
using HandNote.Services.Notification;
using HandNote.Services.Post;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Services.User;
using Microsoft.AspNetCore.Authentication;

namespace HandNote.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            services.AddScoped<IMediaFilesRepository, MediaFilesRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostReactionService, PostReactionService>();
            services.AddScoped<IMediaFilesService, MediaFilesService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }

        public static IServiceCollection AddHighPerformanceJson(this IServiceCollection services)
        {
            services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SigningKey"] ??
                throw new InvalidOperationException("JWT SigningKey required"));

            // Configure authentication schemes
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = "Cookies"; // Required for OAuth
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = false; // Performance optimization
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // Strict timing
                };
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                var googleConfig = configuration.GetSection("Google");
                options.ClientId = googleConfig["ClientId"] ??
                    throw new InvalidOperationException("Google ClientId is required");
                options.ClientSecret = googleConfig["ClientSecret"] ??
                    throw new InvalidOperationException("Google ClientSecret is required");

                // Configure OAuth scopes
                options.Scope.Add("email");
                options.Scope.Add("profile");

                // Configure callback path
                options.CallbackPath = "/api/auth/callback-google";

                // Save tokens for later use if needed
                options.SaveTokens = true;

                // Configure claims mapping
                options.ClaimActions.MapJsonKey("email", "email");
                options.ClaimActions.MapJsonKey("name", "name");
                options.ClaimActions.MapJsonKey("given_name", "given_name");
                options.ClaimActions.MapJsonKey("family_name", "family_name");
                options.ClaimActions.MapJsonKey("picture", "picture");
            })
                .AddCookie("Cookies", options =>
            {
                // Configure cookie authentication for OAuth sign-in
                options.LoginPath = "/api/auth/sign-google";
                options.LogoutPath = "/api/auth/logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });


            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HandNote API",
                    Version = "v1.0",
                    Description = "Architecture - High Performance API"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services, string[] allowedOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                });
            });

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray();

                    var response = new ErrorResponse("Validation failed", errors.ToList());
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static IServiceCollection AddPerformance(this IServiceCollection services)
        {
            // Response compression
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = new[] { "application/json", "text/json" };
            });

            return services;
        }
    }
}