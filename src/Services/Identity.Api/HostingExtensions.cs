using System.Reflection;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RedPhase.Identity.Api.Data;

using Serilog;


namespace RedPhase.Identity.Api;

internal static class HostingExtensions
{

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Default");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore<ConfigurationDbContext>(options =>
           {
               options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                   sql => sql.MigrationsAssembly(migrationsAssembly));
           })
               .AddOperationalStore<PersistedGrantDbContext>(options =>
               {
                   options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                       sql => sql.MigrationsAssembly(migrationsAssembly));
               })
            .AddAspNetIdentity<ApplicationUser>();


        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        app.UseRouting();
        app.UseIdentityServer();


        app.UseAuthorization();

        return app;
    }
}
