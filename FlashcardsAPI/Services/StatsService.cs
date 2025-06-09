using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;

namespace FlashcardsAPI.Services
{
    public class StatsService
    {
        private readonly StatsRepository _statsRepository;
        private readonly StackRepository _stackRepository;

        public StatsService(StatsRepository statsRepository, StackRepository stackRepository)
        {
            _statsRepository = statsRepository;
            _stackRepository = stackRepository;
        }

        public StackLearningStats? GetStackStatsByStackId(int stackId)
        {
            return _statsRepository.GetStackStatsByStackId(stackId);
        }

        public float GetStacksCorrectRate(StackLearningStatsDto stats)
        {
            var stack = _stackRepository.FindStack(stats.StackId);
            if (stack == null || stack.Cards.Count == 0)
            {
                return 0f;
            }

            float correctCount = stats.CurrentScore;
            float total = stack.Cards.Count;

            var correctRate = correctCount / total;
            return correctRate;
        }

        public int UpdateHighestStackScore(StackLearningStatsDto updatedStats)
        {
            var oldStats = _statsRepository.GetStackStatsByStackId(updatedStats.StackId);
           _statsRepository.UpdateHighestStackSore(oldStats, updatedStats);
            var updatedStat = _statsRepository.GetStackStatsByStackId(oldStats.StackId);
            return updatedStat.HighestSore;
        }

        public UserLearningStats? GetUserStatsByUserId(int userId)
        {
            return _statsRepository.GetUserStatsByUserId(userId);
        }
        public DateTime UpdateLastStudyTime(UserLearningStatsDto updatedStats)
        {
            var oldStats = _statsRepository.GetUserStatsByUserId(updatedStats.UserId);
            _statsRepository.UpdateLastStudyTime(oldStats, updatedStats);
            var updatedStat = _statsRepository.GetUserStatsByUserId(oldStats.UserId);
            return updatedStat.LastStudyTime;
        }

        public StackLearningStats AddStackStats(StackLearningStatsDto newStats)
        {
            var stat = new StackLearningStats
            {
                HighestSore = newStats.HighestScore
            };

            _statsRepository.AddStackStats(stat);
            var lastStat = _statsRepository.GetStackStatsByStackId(newStats.StackId);
            return lastStat;
        }

        public UserLearningStats AddUserStats(UserLearningStatsDto newStats)
        {
            var stat = new UserLearningStats
            {
                LastStudyTime = newStats.LastStudyTime
            };

            _statsRepository.AddUserStats(stat);
            var lastStat = _statsRepository.GetUserStatsByUserId(newStats.UserId);
            return lastStat;
        }
    }
}
