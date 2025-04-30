using FlashcardsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Repository
{
    public interface IAuthRepository
    {
        void InsertUser(User user);
        void UpdateUser(User userToUpdate, User updatedUser);
        User? FindUser(string email);
        bool EmailExists(string email);
        bool userNameExists(string userName);
    }
}
