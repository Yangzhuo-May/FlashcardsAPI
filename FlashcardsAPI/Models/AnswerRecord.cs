using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class AnswerRecord
    {
        [Key]
        public int Id { get; set; }

        public int StackId { get; set; }
        public int UserId { get; set; }
        public required DateTime AnsweredAt { get; set; }
        public required float CorrectRate { get; set; }
    }
}
