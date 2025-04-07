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
                _stackRepository.UpdateStack(stack);
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
                _stackRepository.DeleteStack(stackId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
