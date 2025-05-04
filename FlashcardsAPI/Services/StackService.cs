using FlashcardsAPI.Controllers;
using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Services
{
    public class StackService : IStackService
    {
        private readonly ILogger<CardController> _logger;
        private readonly IStackRepository _stackRepository;
        public StackService(ILogger<CardController> logger, IStackRepository stackRepository)
        {
            _logger = logger;
            _stackRepository = stackRepository;
        }

        public List<Stack> GetAllStacks()
        {
            try
            {
                var stacks = _stackRepository.GetAllStacks();
                if(stacks == null)
                {
                    throw new Exception("No stack be found!!");
                }
                return stacks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddStack(string stackName, int userId)
        {
            Stack newStack = new Stack { StackName = stackName, UserId = userId };
            try
            { 
                _stackRepository.InsertStack(newStack); 
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                throw new Exception(innerException);
            }
        }

        public void EditStack(Stack stack)
        {
            try
            {
                var stackDb = _stackRepository.FindStack(stack.StackId);
                if (stackDb == null)
                {
                    throw new Exception("Stack not found!!");
                }
                _stackRepository.UpdateStack(stackDb, stack);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteStack(int stackId)
        {
            try
            {
                var stackDb = _stackRepository.FindStack(stackId);
                if (stackDb == null)
                {
                    throw new Exception("Stack not found!!");
                }
                _stackRepository.DeleteStack(stackDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
