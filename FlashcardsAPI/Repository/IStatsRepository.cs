using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface IStatsRepository
    {
        StackLearningStats? GetStackStatsByStackId(int stackId);
        void AddStackStats(StackLearningStats stats);
        void UpdateHighestStackSore(StackLearningStats learningStats, StackLearningStatsDto updatedStats);

        UserLearningStats? GetUserStatsByUserId(int userId);
        void AddUserStats(UserLearningStats stats);
        void UpdateLastStudyTime(UserLearningStats learningStats, UserLearningStatsDto updatedStats);

    }
}
