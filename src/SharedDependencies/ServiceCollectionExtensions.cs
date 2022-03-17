using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;

using Consul;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;

using StackExchange.Redis.Extensions.System.Text.Json;

namespace RedPhase.SharedDependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGeneralServices<TDbContext>(this IServiceCollection services, ConfigurationBase config) where TDbContext : DbContext
    {
        services.AddCustomCap(config)
                .AddCustomRedis(config)
                .AddCustomDbContext<TDbContext>(config)
                .AddCustomAuthentication(config);

        return services;
    }

    public static IServiceCollection AddCustomCap(this IServiceCollection services, ConfigurationBase config)
    {
        services.AddCap(build =>
        {
            build.UseDashboard();
            build.UseKafka(r =>
            {
                r.Servers = config.ConnectionStrings.Kafka;
                r.MainConfig.Add("allow.auto.create.topics", "true");
            });

            build.UsePostgreSql(r =>
            {
                r.ConnectionString = config.ConnectionStrings.ApplicationDb;
                r.Schema = config.CapConfig.SchemaName;
            });

            build.SucceedMessageExpiredAfter = config.CapConfig.SucceedMessageExpiredAfter;
            build.ConsumerThreadCount = config.CapConfig.ConsumerThreadCount;
            build.ProducerThreadCount = config.CapConfig.ProducerThreadCount;

        });

        return services;
    }

    public static IServiceCollection AddCustomRedis(this IServiceCollection services, ConfigurationBase config)
    {
        var redisConfig = config.Redis;

        services.AddSingleton<StackExchange.Redis.Extensions.Core.ISerializer, SystemTextJsonSerializer>();
        services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfig);

        return services;
    }

    public static IServiceCollection AddCustomDbContext<TDbContext>(this IServiceCollection services, ConfigurationBase config) where TDbContext : DbContext
    {
        return AddCustomDbContext<TDbContext>(services, config, r => r.ApplicationDb);

    }

    public static IServiceCollection AddCustomDbContext<TDbContext>(this IServiceCollection services,
                                                                    ConfigurationBase config,
                                                                    Expression<Func<ConnectionStrings, string>> connectionStringSelector) where TDbContext : DbContext
    {
        var builtSelector = connectionStringSelector.Compile();

        var connectionString = builtSelector(config.ConnectionStrings);

        services.AddDbContext<TDbContext>(builder =>
        {
            var asmName = typeof(TDbContext).Assembly.GetName().Name;
            builder.UseNpgsql(connectionString, options => options.MigrationsAssembly(asmName));
            builder.EnableSensitiveDataLogging();
            builder.EnableDetailedErrors();
            builder.EnableThreadSafetyChecks(false);

        });

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, ConfigurationBase config)
    {
        IdentityModelEventSource.ShowPII = true;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = config.AuthorityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = config.ApiName;

            options.TokenValidationParameters = new()
            {
                ValidateAudience = false,
                ValidateIssuer = false // this should be temporary.
            };
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            var address = configuration.GetValue<string>("Consul:Host");
            consulConfig.Address = new Uri(address);
        }));
        return services;
    }

    public static WebApplication UseConsul(this WebApplication app, ConfigurationBase config)
    {
        var services = app.Services;
        var consulClient = services.GetRequiredService<IConsulClient>();

        var lifetime = services.GetRequiredService<IHostApplicationLifetime>();
        var hostName = Dns.GetHostName();
        var hostUrl = Dns.GetHostAddresses(hostName)
                    .FirstOrDefault(ha => ha.AddressFamily == AddressFamily.InterNetwork)
                    .ToString();
        var registration = new AgentServiceRegistration()
        {
            ID = hostName, //{uri.Port}",
                           // servie name  
          //  Tags = new[] { "Crm" },
            Name = config.ApiName,
            Address = "172.21.160.1", //$"{uri.Host}",
            Port = 5001  // uri.Port
            
        };

        var result = consulClient.Agent.ServiceDeregister(registration.ID).Result;
        var result2 = consulClient.Agent.ServiceRegister(registration).Result;

        lifetime.ApplicationStopping.Register(() =>
        {
            consulClient.Agent.ServiceDeregister(registration.ID);
        });

        return app;
    }


}
