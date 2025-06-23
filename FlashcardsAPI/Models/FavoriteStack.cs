using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class FavoriteStack
    {
        public int StackId { get; set; }
        public required int UserId { get; set; } 

        public Stack Stack { get; set; }
        public User User { get; set; }
    }
}
