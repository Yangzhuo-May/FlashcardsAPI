using FlashcardsAPI.Data;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(ApplicationDbContext context, ILogger<AuthRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User userToUpdate, User updatedUser)
        {

        }

        public User? FindUser(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool userNameExists(string userName)
        {
            return _context.Users.Any(u => u.Username == userName);
        }
    }
}
