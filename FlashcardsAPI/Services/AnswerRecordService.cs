using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;

namespace FlashcardsAPI.Services
{
    public class AnswerRecordService : IAnswerRecordService
    {
        private readonly IAnswerRecordRepository _answerRecordRepository;

        public AnswerRecordService(IAnswerRecordRepository answerRecordRepository)
        {
            _answerRecordRepository = answerRecordRepository;
        }

        public void AddAnswerRecord(AnswerRecord answerRecord)
        {
            _answerRecordRepository.AddAnswerRecord(answerRecord);
        }

        public List<float> GetUserAnswerRecordsByStack(int StackId, int userId)
        {
            var records = _answerRecordRepository.GetUserAnswerRecordsByStack(StackId, userId);
            return records;
        }
    }
}
