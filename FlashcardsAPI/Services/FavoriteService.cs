using FlashcardsAPI.Repository;
using FlashcardsAPI.Models;
using FlashcardsAPI.Dtos;

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
            return FindDbData(userId);
        }

        public List<Stack> AddFavoriteStack(FavoriteStackRequest request)
        {
            _favoriteRepository.AddFavoriteStack(request);
            return FindDbData(request.UserId);
        }

        public List<Stack> DeleteFavoriteStack(FavoriteStackRequest request)
        {
            _favoriteRepository.DeleteFavoriteStack(request);
            return FindDbData(request.UserId);
        }

        private List<Stack> FindDbData(int userId)
        {
            var favoriteStacksId = _favoriteRepository.FindFavoriteStack(userId);
            var favoriteStacks = _stackRepository.FindStacksByIds(favoriteStacksId);
            return favoriteStacks;
        }
    }
}
