namespace FlashcardsAPI.Dtos
{
    public class CardDto
    {
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
