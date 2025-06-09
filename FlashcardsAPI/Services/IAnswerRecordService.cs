using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IAnswerRecordService
    {
        void AddAnswerRecord(AnswerRecord answerRecord);
        List<float> GetUserAnswerRecordsByStack(int StackId, int userId);
    }
}
