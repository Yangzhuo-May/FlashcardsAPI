using System.Security.Claims;

namespace FlashcardsAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new ArgumentException("User ID claim is missing.");
            }
            return int.Parse(userIdString);
        }
    }
}
