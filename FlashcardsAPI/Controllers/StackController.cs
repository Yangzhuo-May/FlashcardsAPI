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
    [Authorize]
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
                var userId = getUserInfoFromToken();
                var stacks = _stackService.GetAllStacks(userId);
                return new OkObjectResult(stacks);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddStack([FromBody] StackRequest request)
        {
            try
            {
                var userId = getUserInfoFromToken();
                _stackService.AddStack(request.NewStackName, userId);
                var updatedStacks = _stackService.GetAllStacks(userId);
                return Ok(new { message = "Stack added successfully", stack = updatedStacks });
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                return BadRequest(new { message = $"An error occurred while saving changes: {innerException}" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditStack([FromBody] StackRequest request)
        {
            try
            {
                _stackService.EditStack(request);
                var userId = getUserInfoFromToken();
                var updatedStacks = _stackService.GetAllStacks(userId);
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
                var userId = getUserInfoFromToken();
                var updatedStacks = _stackService.GetAllStacks(userId);
                return Ok(new { message = "Stack deleted successfully", stack = updatedStacks }); 
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("userinfo")]
        public int getUserInfoFromToken()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdString);
        }
    }
}
