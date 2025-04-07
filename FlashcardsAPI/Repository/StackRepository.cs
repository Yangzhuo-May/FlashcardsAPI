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

        public void UpdateStack(Stack stack)
        {
            var stackDb = FindStack(stack.StackId);
            if (stackDb != null)
            {
                stackDb.StackName = stack.StackName;
                _context.SaveChanges();
            }
        }

        public void DeleteStack(int stackId)
        {
            var stackDb = FindStack(stackId);
            if (stackDb != null)
            {
                _context.Remove(stackDb);
                _context.SaveChanges();
            }
        }

        public Stack FindStack(int stackId)
        {
            Stack stackDb;
            return stackDb = _context.Stacks.Find(stackId);
        }

        public List<Stack> GetAllStacks()
        {
            return _context.Stacks.ToList();
        }
    }
}
