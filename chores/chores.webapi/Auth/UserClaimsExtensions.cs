using System.Security.Claims;

namespace chores.webapi.Auth;

public static class UserClaimsExtensions
{
    public static string[] GetScopes(this ClaimsPrincipal user)
    {
        return user.FindFirst("http://schemas.microsoft.com/identity/claims/scope")?.Value?.Split(' ') ?? [];
    }

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal user)
    {
        return user.FindAll(ClaimTypes.Role).Select(r => r.Value);
    }

    public static bool HasScope(this ClaimsPrincipal user, string scope)
    {
        return user.GetScopes().Contains(scope);
    }

    public static bool HasRole(this ClaimsPrincipal user, string role)
    {
        return user.GetRoles().Contains(role);
    }
}
