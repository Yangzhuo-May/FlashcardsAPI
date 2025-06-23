using System.Security.Claims;
using System.Text.Json;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Extensions;
using FlashcardsAPI.Models;
using FlashcardsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlashcardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly IFavoriteService _favoriteService;
        public FavoriteController(ILogger<CardController> logger, IFavoriteService favoriteService)
        {
            _logger = logger;
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public IActionResult GetFavoriteStackList()
        {
            try
            {
                var userId = User.GetUserId();
                var favoriteStacks = _favoriteService.GetFavoriteByUserId(userId);
                _logger.LogInformation("Favorite Stack of user {UserId}: {FavoriteStacks}", userId, JsonSerializer.Serialize(favoriteStacks));
                return new OkObjectResult(favoriteStacks);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddFavoriteStack([FromBody] FavoriteStackRequest request)
        {
            try
            {
                var favoriteStacks = _favoriteService.AddFavoriteStack(request);
                return new OkObjectResult(new { message = "FavoriteStack added successfully", favorites = favoriteStacks });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteFavoriteStack([FromBody] FavoriteStackRequest request)
        {
            try
            {
                var favoriteStacks = _favoriteService.DeleteFavoriteStack(request);
                return new OkObjectResult(new { message = "FavoriteStack deleted successfully", favorites = favoriteStacks });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
