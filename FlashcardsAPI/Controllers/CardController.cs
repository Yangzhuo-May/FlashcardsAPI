using FlashcardsAPI.Models;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace FlashcardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardService _cardService;
        public CardController(ILogger<CardController> logger, ICardService questionService)
        {
            _logger = logger;
            _cardService = questionService;
        }

        [HttpGet]
        public IActionResult GetAllCards()
        {
            try
            {
                var cards = _cardService.GetAllCards();
                return new OkObjectResult(cards);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("{stackId}")]
        public IActionResult GetCardsByStack(int stackId)
        {
            try
            {
                var cards = _cardService.GetCardsByStackId(stackId);
                return new OkObjectResult(cards);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCard([FromBody] CardDto card)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                Console.WriteLine("⚠️ User ID is null");
                return BadRequest("User ID is missing.");
            }

            var userIdString = userIdClaim.Value;

            var userId = int.Parse(userIdString);

            if (card == null)
            {
                Console.WriteLine("⚠️ card is NULL");
                return BadRequest("Invalid input data.");
            }

            Console.WriteLine($"✅ Received card: {card.Question}, StackId: {card.StackId}");

            _logger.LogInformation("Received request to create a new card with the following data: {CardData}", card);
            try
            {
                _cardService.AddCard(card, userId);

                _logger.LogInformation("Successfully created a new card: {CreatedCardData}", card);

                return Ok(new { message = "Card added successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new card.");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditCard(int id, CardDto card)
        {
            try
            {
                _cardService.EditCard(id, card);
                return Ok(new { message = "Card edited successfully" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            try
            {
                _cardService.DeleteCard(id);
                return Ok(new { message = "Card deleted successfully" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
