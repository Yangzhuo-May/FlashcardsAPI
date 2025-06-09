namespace FlashcardsAPI.Dtos
{
    public class AnswerRecordDto
    {
        public int StackId { get; set; }
        public int UserId { get; set; }
        public required DateTime AnsweredAt { get; set; }
        public required float CorrectRate { get; set; }
    }
}
