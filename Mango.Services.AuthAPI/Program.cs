using Serilog.Events;
using Serilog;
using Mango.Services.AuthAPI.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Service.Interface;
using Mango.Services.AuthAPI.Service;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Auth API Service ");

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
    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

    builder.Services.AddControllers();
    builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

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
