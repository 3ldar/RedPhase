using System.Security.Claims;

using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace RedPhase.Identity.Api.Services;

public class ProfileService : IProfileService
{
    public ProfileService()
    {

    }

    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        context.IssuedClaims = new List<Claim> { new Claim("RoleId","1"),
            new Claim("PositionId","2"),
            new Claim("UserId","3"),
            new Claim("OrganizationId","4"),
            new Claim("UserName","IK1001"),
            new Claim("SessionId",Guid.NewGuid().ToString())
        };

        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
