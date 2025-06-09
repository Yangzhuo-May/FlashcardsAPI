using System.Security.Claims;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Extensions;
using FlashcardsAPI.Models;
using FlashcardsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatController : ControllerBase
    {
        private readonly IStatsService _statsService;
        public StatController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("Stack")]
        public IActionResult GetStackStatsByStackId(int stackId)
        {
            try
            {
                var stackStat = _statsService.GetStackStatsByStackId(stackId);
                return new OkObjectResult(stackStat);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("User")]
        public IActionResult GetUserStatsByUserId()
        {
            try
            {
                var userId = User.GetUserId();
                var userStat = _statsService.GetUserStatsByUserId(userId);
                return new OkObjectResult(userStat);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("CorrectRate")]
        public IActionResult GetStacksCorrectRate([FromQuery] StackLearningStatsDto stats)
        {
            try
            {
                var userStat = _statsService.GetStacksCorrectRate(stats);
                return new OkObjectResult(userStat);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("Stack")]
        public IActionResult AddStackStat(StackLearningStatsDto statsDto)
        {
            try
            {
                var respond = _statsService.AddStackStats(statsDto);
                return new OkObjectResult(respond);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("User")]
        public IActionResult AddUserStat(UserLearningStatsDto statsDto)
        {
            try
            {
                var respond = _statsService.AddUserStats(statsDto);
                return new OkObjectResult(respond);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut("Stack")]
        public IActionResult UpdateHighestStackScore(StackLearningStatsDto statsDto)
        {
            try
            {
                var updatedStat = _statsService.UpdateHighestStackScore(statsDto);
                return new OkObjectResult(updatedStat);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut("User")]
        public IActionResult UpdateLastStudyTime(UserLearningStatsDto statsDto)
        {
            try
            {
                var updatedStat = _statsService.UpdateLastStudyTime(statsDto);
                return new OkObjectResult(updatedStat);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
