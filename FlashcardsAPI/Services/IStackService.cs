using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IStackService
    {
        void AddStack(StackRequest request, int userId);
        void EditStack(StackRequest request);
        void UpdateStackPublicStatus(int id, StackRequest request);
        void DeleteStack(int stackId);
        StackRequest FindStackById(int id);
        List<Stack> GetAllStacks(int userId);
        List<Stack> GetAllPublicStacks();
    }
}
