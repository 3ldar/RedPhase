using Duende.IdentityServer.Models;


namespace RedPhase.Identity.Api;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
            new ApiScope("crm.api"),
            new ApiScope("dms.api"),
            new ApiScope("krn.api"),
            new ApiScope("fnc.api"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "spa",
                ClientName = "RedPhase SPA Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedScopes = { "scope1","crm.api","dms.api","krn.api","fnc.api" },
                RequireClientSecret = false,

            }
        };
}


