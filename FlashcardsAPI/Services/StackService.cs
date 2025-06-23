using Azure.Core;
using FlashcardsAPI.Controllers;
using FlashcardsAPI.Dtos;
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

        public List<Stack> GetAllStacks(int userId)
        {
            try
            {
                var stacks = _stackRepository.GetAllStacks(userId);
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

        public List<Stack> GetAllPublicStacks()
        {
            try
            {
                var stacks = _stackRepository.GetAllPublicStacks();
                return stacks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StackRequest FindStackById(int id)
        {
            var stackDb = _stackRepository.FindStack(id);
            StackRequest stackDto = new StackRequest
            {
                StackId = stackDb.StackId,
                NewStackName = stackDb.StackName,
                IsPublic = stackDb.IsPublic,
            };
            return stackDto;
        }

        public void AddStack(StackRequest request, int userId)
        {
            Stack newStack = new Stack 
            { 
                StackName = request.NewStackName, 
                UserId = userId,
                IsPublic = request.IsPublic ?? false,
            };
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

        public void EditStack(StackRequest request)
        {
            try
            {
                var stackDb = _stackRepository.FindStack(request.StackId);
                if (stackDb == null)
                {
                    throw new Exception("Stack not found!!");
                }
                _stackRepository.UpdateStack(stackDb, request.NewStackName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStackPublicStatus(int id, StackRequest request)
        {
            try
            {
                var stackDb = _stackRepository.FindStack(id);
                var isPublic = request.IsPublic;
                if (stackDb == null || isPublic == null)
                {
                    throw new Exception("Stack not found!!");
                }

                    _stackRepository.UpdateStackPublicStatus(stackDb, isPublic ?? false);
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
