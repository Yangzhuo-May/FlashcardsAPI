using FlashcardsAPI.Repository;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IStackRepository _stackRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository, IStackRepository stackRepository)
        {
            _favoriteRepository = favoriteRepository;
            _stackRepository = stackRepository;
        }

        public List<Stack> GetFavoriteByUserId(int userId)
        {
            var favoriteStacksId = _favoriteRepository.FindFavoriteStack(userId);
            var favoriteStacks = _stackRepository.FindStacksByIds(favoriteStacksId);
            return favoriteStacks;
        }
    }
}
