using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthLib.Extensions {
    public static class ClaimsPrincipalExtensions {
        public static string GetUserId(this ClaimsPrincipal principal) {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }


        public static string GetUserId(this IEnumerable<Claim> claims) {
            if (claims == null || claims.Count() <= 0)
                throw new ArgumentNullException(nameof(claims));

            return claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
