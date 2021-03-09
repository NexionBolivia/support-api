using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Support.API.Services.Extensions
{
    public static class UserExtensions
    {
        public static string GetCurrentUserName(this HttpContext context)
        {
            string userId = "";

            var firstIdentity = context.User.Identities?.FirstOrDefault()?.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);
            if (firstIdentity != null)
                userId = firstIdentity.Value;

            return userId;
        }
    }
}
