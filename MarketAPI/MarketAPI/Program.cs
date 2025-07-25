using MarketAPI.Custom;
using MarketAPI.Data;
using MarketAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System;
using System.Reflection;
using System.Text;


var logger = NLog.LogManager.GetCurrentClassLogger();
logger.Debug("Init main");


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


    builder.Services.AddHealthChecks();


    builder.Services.AddDbContext<MarketDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("MarketApiContext")));

    // Add services to the container.

    builder.Services.AddControllers();

    // Configuración de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Market",
            Version = "v1",
            Description = "Market App",
            Contact = new OpenApiContact
            {
                Name = "Leonardo Cortes",
                Email = "DrLeoCortex@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Licencia MIT"
            }
        });

        // Incluir comentarios XML (opcional)
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });


    builder.Services.AddSingleton<Utilities>();


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(config =>
    {
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

    builder.Services.AddScoped<IAuthService, AuthService>();



    var app = builder.Build();

    app.MapHealthChecks("/health");

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Market v1");
            c.DisplayRequestDuration(); // Mostrar duración de las solicitudes
            c.EnableTryItOutByDefault(); // Habilitar "Try it out" por defecto
        });
    }

    app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5173") // frontend URL
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials());

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "An error occurred while starting the application.");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}