using FlashcardsAPI.Data;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<int> FindFavoriteStack(int userId)
        {
            return _context.FavoriteStacks
                .Where(f => f.UserId == userId)
                .Select(f => f.StackId)
                .ToList();
        }

        public void AddFavoriteStack(FavoriteStackRequest request)
        {
            var favoriteStack = new FavoriteStack 
            { 
                UserId = request.UserId, 
                StackId = request.StackId 
            };
            _context.FavoriteStacks.Add(favoriteStack);
            _context.SaveChanges();
        }

        public void DeleteFavoriteStack(FavoriteStackRequest request)
        {
            var favorite = _context.FavoriteStacks
        .FirstOrDefault(f => f.UserId == request.UserId && f.StackId == request.StackId);

            if (favorite != null)
            {
                _context.FavoriteStacks.Remove(favorite);
                _context.SaveChanges();
            }
        }
    }
}
