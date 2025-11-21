using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Repositories;
using Security.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
Env.Load();

// ========== CONFIGURACIÓN DE PUERTO ==========
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// ========== CONFIGURACIÓN DE SERVICIOS ==========
builder.Services.AddControllers();

// ========== SWAGGER ==========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Animals API",
        Version = "v1",
        Description = "API para gestión de animales con autenticación JWT"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
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
            new string[] {}
        }
    });
});

// ========== CORS ==========
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p => p
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// ========== IN-MEMORY DATABASE (SOLUCIÓN TEMPORAL) ==========
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("AnimalsDB"));

// ========== JWT AUTHENTICATION ==========
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? "tu_clave_secreta_minima_32_caracteres_123456789012";
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "AnimalsAPI";
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "AnimalsClient";

if (jwtKey.Length < 32)
{
    jwtKey = jwtKey.PadRight(32, '0');
}

var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero
        };
    });

// ========== AUTHORIZATION ==========
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
});

// ========== DEPENDENCY INJECTION ==========
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

var app = builder.Build();

// ========== CONFIGURACIÓN PIPELINE ==========
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Animals API v1");
    });
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ========== INICIALIZAR DATOS DE PRUEBA ==========
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Agregar datos de prueba
    if (!db.Animals.Any())
    {
        db.Animals.AddRange(
            new Security.Models.Animal
            {
                Id = Guid.NewGuid(),
                Name = "León",
                Species = "Panthera leo",
                Age = 5,
                Habitat = "Sabana",
                ConservationStatus = "Vulnerable"
            },
            new Security.Models.Animal
            {
                Id = Guid.NewGuid(),
                Name = "Elefante Africano",
                Species = "Loxodonta africana",
                Age = 15,
                Habitat = "Sabana y Bosques",
                ConservationStatus = "En Peligro"
            },
            new Security.Models.Animal
            {
                Id = Guid.NewGuid(),
                Name = "Tigre de Bengala",
                Species = "Panthera tigris tigris",
                Age = 8,
                Habitat = "Bosques Tropicales",
                ConservationStatus = "En Peligro"
            }
        );
        db.SaveChanges();
    }

    if (!db.Users.Any())
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("123456");
        db.Users.Add(new Security.Models.User
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            Email = "admin@test.com",
            PasswordHash = hashedPassword,
            Role = "Admin"
        });
        db.SaveChanges();
    }
}

app.Run();