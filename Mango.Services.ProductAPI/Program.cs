using Mango.Services.ProductAPI.Extensions;
using Mango.Services.ProductAPI.AutoMapper;
using Mango.Services.ProductAPI.DB;
using Mango.Services.ProductAPI.Repository;
using Mango.Services.ProductAPI.Repository.Interfaces;
using Mango.Services.ProductAPI.Service;
using Mango.Services.ProductAPI.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Product API Service ");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Add Logging
    builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .WriteTo.Console());

    // Add Db Connection
    builder.Services.AddDbContext<AppDbContext>(option =>
    {
        option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    // Add Controllers
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => {
        options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JET-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id=JwtBearerDefaults.AuthenticationScheme
                    }
                }, new string[]{}
            }
        });
    });

    builder.AddAppAuthentication();
    builder.Services.AddAuthorization();

    // Adding Mapping Profile
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // Add Unit Of Work and Repository
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    // Add services
    builder.Services.AddScoped<IProductService, ProductService>();

    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Add Logging for Request
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    ApplyMigration();

    app.Run();

    void ApplyMigration()
    {
        using var scope = app.Services.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}