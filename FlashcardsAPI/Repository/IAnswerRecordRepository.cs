using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface IAnswerRecordRepository
    {
        void AddAnswerRecord(AnswerRecord answerRecord);
        List<float> GetUserAnswerRecordsByStack(int StackId, int userId);
    }
}
