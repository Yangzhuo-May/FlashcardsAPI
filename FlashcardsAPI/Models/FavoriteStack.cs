using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class FavoriteStack
    {
        [Key]
        public int FavoriteId { get; set; }

        public int StackId { get; set; }
        public required int UserId { get; set; } 

        public Stack Stack { get; set; }
        public User User { get; set; }
    }
}
