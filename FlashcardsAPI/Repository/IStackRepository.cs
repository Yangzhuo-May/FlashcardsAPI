using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface IStackRepository
    {
        void InsertStack(Stack stack);
        void UpdateStack(Stack stack);
        void DeleteStack(int StackID);
        Stack FindStack(int id);
        List<Stack> GetAllStacks();
    }
}
