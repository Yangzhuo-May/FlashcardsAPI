using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IStackService
    {
        void AddStack(string stackName, int userId);
        void EditStack(StackRequest request);
        void DeleteStack(int stackId);
        List<Stack> GetAllStacks(int userId);
        List<Stack> GetAllPublicStacks();
    }
}
