using System.Security.Claims;

namespace FlashcardsAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirstValue("userId");
            return int.Parse(userIdString);
        }
    }
}
