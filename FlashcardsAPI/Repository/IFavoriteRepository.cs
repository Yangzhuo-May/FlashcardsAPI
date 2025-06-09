using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface IFavoriteRepository
    {
        List<int> FindFavoriteStack(int userId);
    }
}
