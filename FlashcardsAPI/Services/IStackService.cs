using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IStackService
    {
        void AddStack(string stackName, int userId);
        void EditStack(Stack stack);
        void DeleteStack(int stackId);
        List<Stack> GetAllStacks();
    }
}
