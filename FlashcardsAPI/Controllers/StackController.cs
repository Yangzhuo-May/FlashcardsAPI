using System.Security.Claims;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;
using FlashcardsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackController : ControllerBase
    {
        private readonly IStackService _stackService;
        public StackController(IStackService stackService)
        {
            _stackService = stackService;
        }

        [HttpGet]
        public IActionResult GetAllStacks()
        {
            try
            {
                var stacks = _stackService.GetAllStacks();
                return new OkObjectResult(stacks);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddStack([FromBody] AddStackRequest request)
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = int.Parse(userIdString);
                _stackService.AddStack(request.newStackName, userId);
                var updatedStacks = _stackService.GetAllStacks();
                return Ok(new { message = "Stack added successfully", stack = updatedStacks });
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                return BadRequest(new { message = $"An error occurred while saving changes: {innerException}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditStack(Stack stack)
        {
            try
            {
                _stackService.EditStack(stack);
                var updatedStacks = _stackService.GetAllStacks();
                return Ok(new { message = "Stack edited successfully", stack = updatedStacks });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete("{stackId}")]
        public IActionResult DeleteStack(int stackId)
        {
            try
            {
                _stackService.DeleteStack(stackId);
                var updatedStacks = _stackService.GetAllStacks();
                return Ok(new { message = "Stack deleted successfully", stack = updatedStacks }); 
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
