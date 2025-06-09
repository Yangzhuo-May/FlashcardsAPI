using FlashcardsAPI.Data;
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
    }
}
