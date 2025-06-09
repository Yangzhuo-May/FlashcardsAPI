using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface IStackRepository
    {
        void InsertStack(Stack stack);
        void UpdateStack(Stack stackToUpdate, string updatedStack);
        void DeleteStack(Stack stack);
        Stack? FindStack(int id);
        List<Stack> FindStacksByIds(List<int> stackIds);
        List<Stack> GetAllStacks(int userId);
    }
}
