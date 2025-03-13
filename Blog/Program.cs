using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Service;
using DataAccessLayer.Data;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inject UnitOfWork containing all repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Inject AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Inject services
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger to support JWT authentication
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API", Version = "v1" });

    // Add JWT authentication support in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token in the format: Bearer <token>"
    });

    // Apply the security requirement only to endpoints with the [Authorize] attribute
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

// Identity Credentials
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;

    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
}).AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Print JWT key during startup for debugging
Console.WriteLine($"JWT Key from configuration: {builder.Configuration["Jwt:Key"]}");
Console.WriteLine($"JWT Issuer from configuration: {builder.Configuration["Jwt:Issuer"]}");
Console.WriteLine($"JWT Audience from configuration: {builder.Configuration["Jwt:Audience"]}");

// Add JWT configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        // Use ClockSkew to account for slight time differences between servers
        ClockSkew = TimeSpan.Zero
    };

    // Enhanced logging for debugging
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            Console.WriteLine($"Authorization header: {context.Request.Headers["Authorization"]}");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.GetType().Name}: {context.Exception.Message}");
            if (context.Exception.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {context.Exception.InnerException.Message}");
            }
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully.");
            var token = context.SecurityToken as JwtSecurityToken;
            if (token != null)
            {
                Console.WriteLine($"Token issuer: {token.Issuer}");
                Console.WriteLine($"Token audience: {token.Audiences.FirstOrDefault()}");
                Console.WriteLine($"Token expiry: {token.ValidTo}");
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"Challenge issued: {context.Error}, {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };

    // Set to false to avoid redirecting API calls to login pages
    options.SaveToken = true;
});

// Add authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Add middleware to print the current request path for debugging
    app.Use(async (context, next) =>
    {
        Console.WriteLine($"Request path: {context.Request.Path}, Method: {context.Request.Method}");

        // Log Authorization header if present
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            Console.WriteLine("Authorization header present");
        }

        await next();
    });
}

app.UseHttpsRedirection();

// Add authentication middleware (must come before UseAuthorization)
app.UseAuthentication();

// Add authorization middleware
app.UseAuthorization();

app.MapControllers();

// Print application URLs for easy reference
if (app.Environment.IsDevelopment())
{
    var urls = builder.WebHost.GetSetting("urls")?.Split(";");
    if (urls != null)
    {
        Console.WriteLine("Application running at:");
        foreach (var url in urls)
        {
            Console.WriteLine($"  {url}");
        }
        Console.WriteLine($"JWT Issuer configured as: {builder.Configuration["Jwt:Issuer"]}");
        Console.WriteLine($"JWT Audience configured as: {builder.Configuration["Jwt:Audience"]}");
        Console.WriteLine("Make sure these match your application URL!");
    }
}

app.Run();

// Custom Swagger operation filter to apply lock icons only to [Authorize] endpoints
public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if the endpoint has the [Authorize] attribute
        var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>()
            .Any();

        if (hasAuthorize)
        {
            // Add the lock icon to the endpoint
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
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
                }
            };
        }
    }
}