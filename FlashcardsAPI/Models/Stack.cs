using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class Stack
    {
        [Key]
        public int StackId { get; set; }

        public required string StackName { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public bool IsProficient { get; set; }
        public bool IsPublic { get; set; }
        public int FavoriteCount { get; set; }
        public required int UserId { get; set; }
        public User User { get; set; }
    }
}
