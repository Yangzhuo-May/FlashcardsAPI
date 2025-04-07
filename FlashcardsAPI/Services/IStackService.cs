using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IStackService
    {
        void AddStack(Stack stack);
        void EditStack(Stack stack);
        void DeleteStack(int stackId);
        List<Stack> GetAllStacks();
    }
}
