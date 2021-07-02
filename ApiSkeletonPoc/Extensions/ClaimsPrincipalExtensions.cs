using System;
using System.Security.Claims;

namespace ApiSkeletonPoc.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClientId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.UserData)?.Value;
        }
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Sid)?.Value;
        }
        public static string GetClientName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.GivenName)?.Value;
        }
    }
}
