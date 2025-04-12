using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FlashcardsAPI.Repository
{
    public class StackRepository : IStackRepository
    {
        private readonly ApplicationDbContext _context;

        public StackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertStack(Stack stack)
        {
            _context.Stacks.Add(stack);
            _context.SaveChanges();
        }

        public void UpdateStack(Stack stackToUpdate, Stack updatedStack)
        {
            stackToUpdate.StackName = updatedStack.StackName;
            _context.SaveChanges();
        }

        public void DeleteStack(Stack stack)
        { 
            _context.Remove(stack);
            _context.SaveChanges();
        }

        public Stack? FindStack(int stackId)
        {
            return _context.Stacks.Find(stackId) ?? null;
        }

        public List<Stack> GetAllStacks()
        {
            return _context.Stacks.ToList();
        }
    }
}
