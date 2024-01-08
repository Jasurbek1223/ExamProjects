using AirBnb.Application.Common.Locations.Services;
using AirBnb.Infrastructure.Common.Caching;
using AirBnb.Infrastructure.Common.Locations.Services;
using AirBnb.Infrastructure.Common.Settings;
using AirBnb.Persistence.Caching;
using AirBnb.Persistence.DbContexts;
using AirBnb.Persistence.Repositories;
using AirBnb.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Api.Configuration;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();

        return builder;
    }

    private static WebApplicationBuilder AddCustomCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
        });

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder;
    }

    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = "Redis";

            options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
        });

        builder.Services.AddSingleton<ICacheBroker, RedisCacheBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddLocationsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection(nameof(UrlSettings)));

        //Registering db contexts
        builder.Services.AddDbContext<LocationsDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        //Register repositories
        builder.Services
            .AddScoped<ILocationRepository, LocationRepository>()
            .AddScoped<ILocationCategoryRepository, LocationCategoryRepository>();


        //register foundation data access services
        builder.Services
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<ILocationCategoryService, LocationCategoryService>();

        //other helper services
        builder.Services.AddScoped<IUrlService, UrlService>();

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        app.UseStaticFiles();

        return app;
    }

    private static WebApplication UseCors(this WebApplication app)
    {
        app.UseCors("CorsPolicy");

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI();

        return app;
    }
}