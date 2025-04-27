namespace FlashcardsAPI.Dtos
{
    public class CardDto
    {
        public required string Question { get; set; }
        public required  string[] Answers { get; set; }
        public required string CorrectAnswer { get; set; }
        public required int StackId { get; set; }
    }
}
