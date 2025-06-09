using FlashcardsAPI.Data;
using FlashcardsAPI.Models;
using FlashcardsAPI.Dtos;

namespace FlashcardsAPI.Repository
{
    public class StatsRepository : IStatsRepository
    {
        private readonly ApplicationDbContext _context;

        public StatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public StackLearningStats? GetStackStatsByStackId(int stackId)
        {
            return _context.StackLearningStats.Find(stackId);
        }

        public void AddStackStats(StackLearningStats stats)
        {
            _context.StackLearningStats.Add(stats);
            _context.SaveChanges();
        }

        public void UpdateHighestStackSore(StackLearningStats learningStats, StackLearningStatsDto updatedStats)
        {
            learningStats.HighestSore = updatedStats.HighestScore;
            _context.SaveChanges();
        }

        public UserLearningStats? GetUserStatsByUserId(int userId)
        {
            return _context.UserLearningStats.Find(userId);
        }

        public void AddUserStats(UserLearningStats stats)
        {
            _context.UserLearningStats.Add(stats);
            _context.SaveChanges();
        }

        public void UpdateLastStudyTime(UserLearningStats learningStats, UserLearningStatsDto updatedStats)
        {
            learningStats.LastStudyTime = updatedStats.LastStudyTime;
            _context.SaveChanges();
        }
    }
}
