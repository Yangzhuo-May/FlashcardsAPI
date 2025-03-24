using FlashcardsAPI.Models;
using FlashcardsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FlashcardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController
    {
        private readonly IQuestionService _questionService;
        public CardController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public IActionResult AddCard(Question question)
        {
            try
            {
                _questionService.AddCard(question);
                return new OkObjectResult("Card added successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditCard(int id, Question question)
        {
            try
            {
                _questionService.EditCard(id, question);
                return new OkObjectResult("Card edited successfully");
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
                _questionService.DeleteCard(id);
                return new OkObjectResult("Card deleted successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
