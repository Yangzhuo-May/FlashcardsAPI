using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Services
{
    public class StackService : IStackService
    {
        private readonly IStackRepository _stackRepository;
        public StackService(IStackRepository stackRepository)
        {
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

        public void AddStack(Stack stack)
        {
            try
            { 
                _stackRepository.InsertStack(stack); 
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
