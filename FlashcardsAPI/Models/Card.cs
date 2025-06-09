using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public required string Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public required int StackId { get; set; }
        public Stack Stack { get; set; }
    }
}
