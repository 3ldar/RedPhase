using System.Security.Claims;

using Duende.IdentityServer.EntityFramework.Mappers;

using IdentityModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RedPhase.Identity.Api.Data;

using Serilog;

namespace RedPhase.Identity.Api;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {

        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var context2 = scope.ServiceProvider.GetService<ConfigurationDbContext>();
        context2.Database.Migrate();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

   

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var alice = userMgr.FindByNameAsync("redqueen").Result;
        if (alice == null)
        {
            alice = new ApplicationUser
            {
                UserName = "redqueen",
                Email = "redqueen@birevim.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("redqueen created");
        }
        else
        {
            Log.Debug("redqueen already exists");
        }

        using var scope2 = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

        EnsureSeedData(context2);
    }

    private static void EnsureSeedData(ConfigurationDbContext context)
    {
        if (!context.Clients.Any())
        {
            Log.Debug("Clients being populated");
            foreach (var client in Config.Clients.ToList())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("Clients already populated");
        }

        if (!context.IdentityResources.Any())
        {
            Log.Debug("IdentityResources being populated");
            foreach (var resource in Config.IdentityResources.ToList())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("IdentityResources already populated");
        }

        if (!context.ApiScopes.Any())
        {
            Log.Debug("ApiScopes being populated");
            foreach (var resource in Config.ApiScopes.ToList())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("ApiScopes already populated");
        }
    }
}
