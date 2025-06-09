using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
using FlashcardsAPI.Controllers;

namespace FlashcardsAPI.Repository
{
    public class StackRepository : IStackRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CardController> _logger;

        public StackRepository(ILogger<CardController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void InsertStack(Stack stack)
        {
            _context.Stacks.Add(stack);
            _context.SaveChanges();
        }

        public void UpdateStack(Stack stackToUpdate, string updatedStack)
        {
            stackToUpdate.StackName = updatedStack;
            _context.SaveChanges();
        }

        public void DeleteStack(Stack stack)
        { 
            _context.Remove(stack);
            _context.SaveChanges();
        }

        public Stack? FindStack(int stackId)
        {
            _logger.LogInformation("🔍Executing FindStack for StackId: {StackId}", stackId);
            Console.WriteLine($"🔍Executing FindStack with StackId: {stackId}");
            return _context.Stacks.Find(stackId) ?? null;
        }

        public List<Stack> FindStacksByIds(List<int> stackIds)
        {
            return _context.Stacks
                .Where(s => stackIds.Contains(s.StackId))
                .ToList();
        }

        public List<Stack> GetAllStacks(int userId)
        {
            return _context.Stacks.Where(s => s.UserId == userId).ToList();
        }
    }
}
