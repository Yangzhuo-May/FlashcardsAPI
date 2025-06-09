using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class StackLearningStats
    {
        [Key]
        public int StackId { get; set; }

        public int UserId { get; set; }
        public int HighestSore {  get; set; }

        public User User { get; set; }
        public Stack Stack { get; set; }
    }
}
