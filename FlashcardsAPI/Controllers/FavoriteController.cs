using System.Security.Claims;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Extensions;
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
    public class FavoriteController : ControllerBase
    {
       private readonly IFavoriteService _favoriteService;
        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public IActionResult GetFavoriteStackList()
        {
            try
            {
                var userId = User.GetUserId();
                var favoriteStacks = _favoriteService.GetFavoriteByUserId(userId);
                return new OkObjectResult(favoriteStacks);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
