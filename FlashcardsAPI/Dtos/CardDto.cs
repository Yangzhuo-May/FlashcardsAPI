using FlashcardsAPI.Models;

namespace FlashcardsAPI.Dtos
{
    public class CardDto
    {
        public required int CardId { get; set; }
        public required string Question { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
        public required int StackId { get; set; }
    }
}
