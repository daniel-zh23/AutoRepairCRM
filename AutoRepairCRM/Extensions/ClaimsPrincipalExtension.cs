using System.Security.Claims;

namespace AutoRepairCRM.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string Id(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}