using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IFavoriteService
    {
        List<Stack> GetFavoriteByUserId(int userId);
        List<Stack> AddFavoriteStack(FavoriteStackRequest request);
        List<Stack> DeleteFavoriteStack(FavoriteStackRequest request);
    }
}
