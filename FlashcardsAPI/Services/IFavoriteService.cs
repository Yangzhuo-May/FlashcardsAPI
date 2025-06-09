using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IFavoriteService
    {
        List<Stack> GetFavoriteByUserId(int userId);
    }
}
