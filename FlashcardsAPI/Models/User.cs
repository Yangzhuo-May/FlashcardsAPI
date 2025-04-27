using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required DateTime RegistrationDate { get; set; }
    }
}
