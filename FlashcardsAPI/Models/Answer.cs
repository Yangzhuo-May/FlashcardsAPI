using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        public required string AnswerText { get; set; }
        public required bool IsCorrect { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
