using FlashcardsAPI.Data;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;
using Xunit;

namespace FlashcardsAPI.Repository
{
    public class AnswerRecordRepository : IAnswerRecordRepository
    {
        private readonly ApplicationDbContext _context;
        public AnswerRecordRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAnswerRecord(AnswerRecord answerRecord)
        {
            var existingRecords = _context.AnswerRecords
                .Where(r => r.UserId == answerRecord.UserId && r.StackId == answerRecord.StackId)
                .OrderByDescending(r => r.AnsweredAt)
                .ToList();

            if (existingRecords.Count >= 3)
            {
                var oldest = existingRecords.Last();
                _context.AnswerRecords.Remove(oldest);
            }

            _context.AnswerRecords.Add(answerRecord);
            _context.SaveChanges();
        }

        public List<float> GetUserAnswerRecordsByStack(int stackId, int userId)
        {
            var records = _context.AnswerRecords
                .Where(r => r.UserId == userId && r.StackId == stackId)
                .OrderByDescending(r => r.AnsweredAt)
                .Select(r => r.CorrectRate)
                .ToList();
            return records;
        }
    }
}
