using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface IStackRepository
    {
        void InsertStack(Stack stack);
        void UpdateStack(Stack stackToUpdate, Stack updatedStack);
        void DeleteStack(Stack stack);
        Stack? FindStack(int id);
        List<Stack> GetAllStacks();
    }
}
