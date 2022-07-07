using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace RedPhase.SharedDependencies;

public static class HttpContextExtensions
{
    public static TokenUserInfo GetUserInfo(this HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"];

        if (!authHeader.Any())
        {
            return null;
        }

        var token = authHeader[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
        var handler = new JwtSecurityTokenHandler();
        var security = handler.ReadToken(token) as JwtSecurityToken;
        var userInfo = GetUserInfoFromClaims(security.Claims);

        return userInfo;
    }

    private static TokenUserInfo GetUserInfoFromClaims(IEnumerable<Claim> claims)
    {
        var userInfo = new TokenUserInfo();
        var properties = typeof(TokenUserInfo).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        foreach (var prop in properties)
        {
            var claim = claims.FirstOrDefault(r => r.Type == prop.Name);
            prop.SetValue(userInfo, GetValue(claim.Value, prop.PropertyType));
        }

        return userInfo;
    }

    private static object GetValue(string v, Type targetType) => targetType.Name switch
    {
        "Int32" => int.Parse(v),
        "String" => v,
        "Guid" => Guid.Parse(v),
        _ => null,
    };
}
