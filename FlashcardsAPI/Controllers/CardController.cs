using FlashcardsAPI.Models;
using FlashcardsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FlashcardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardController(ICardService questionService)
        {
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
        public IActionResult AddCard(Card card)
        {
            try
            {
                _cardService.AddCard(card);
                return Ok(new { message = "Card added successfully" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditCard(int id, Card card)
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
