using System.Text;
using Mango.Services.CouponAPI.AutoMapper;
using Mango.Services.CouponAPI.DB;
using Mango.Services.CouponAPI.Repository;
using Mango.Services.CouponAPI.Repository.Interface;
using Mango.Services.CouponAPI.Service;
using Mango.Services.CouponAPI.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Coupon API Service ");

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
    builder.Services.AddSwaggerGen();

    // configuration authentication
    var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret");
    var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
    var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");

    var key = Encoding.ASCII.GetBytes(secret!);

    builder.Services.AddAuthentication(x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x => {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            ValidateAudience = true
        };
    });
    builder.Services.AddAuthorization();

    // Adding Mapping Profile
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // Add Unit Of Work and Repository
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    // Add services
    builder.Services.AddScoped<ICouponService, CouponService>();

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