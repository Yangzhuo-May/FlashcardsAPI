using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface IFavoriteRepository
    {
        List<int> FindFavoriteStack(int userId);
        void AddFavoriteStack(FavoriteStackRequest request);
        void DeleteFavoriteStack(FavoriteStackRequest request);
    }
}
