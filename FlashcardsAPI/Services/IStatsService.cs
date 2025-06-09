using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IStatsService
    {
        StackLearningStats? GetStackStatsByStackId(int stackId);
        StackLearningStats AddStackStats(StackLearningStatsDto newStats);
        int UpdateHighestStackScore(StackLearningStatsDto updatedStats);
        float GetStacksCorrectRate(StackLearningStatsDto stats);

        UserLearningStats? GetUserStatsByUserId(int userId);
        UserLearningStats AddUserStats(UserLearningStatsDto newStats);
        DateTime UpdateLastStudyTime(UserLearningStatsDto updatedStats);
    }
}
