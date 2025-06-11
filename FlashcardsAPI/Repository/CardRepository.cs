using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
using FlashcardsAPI.Dtos;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CardRepository> _logger;

        public CardRepository(ApplicationDbContext context, ILogger<CardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void InsertCard(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }

        public void InsertCards(List<Card> cards)
        {
            _context.Cards.AddRange(cards);
            _context.SaveChanges();
        }

        public void UpdateCard(Card existingCard, CardDto updatedCardDto)
        {
            _logger.LogInformation("Start updating card，CardId: {CardId}", existingCard.CardId);
            existingCard.Question = updatedCardDto.Question;
            existingCard.StackId = updatedCardDto.StackId;

            if (existingCard.Answers != null)
            {
                _logger.LogInformation("Delete {Count} answers", existingCard.Answers.Count);
                _context.Answers.RemoveRange(existingCard.Answers);
                existingCard.Answers.Clear();
            }
            else
            {
                _logger.LogInformation("No answers，initial list");
                existingCard.Answers = new List<Answer>();
            }

            foreach (var answerDto in updatedCardDto.Answers)
            {
                _logger.LogInformation("Add answer：{AnswerText}，IsCorrect: {IsCorrect}",
             answerDto.AnswerText, answerDto.IsCorrect);

                existingCard.Answers.Add(new Answer
                {
                    AnswerText = answerDto.AnswerText,
                    IsCorrect = answerDto.IsCorrect
                });
            }

            _context.SaveChanges();
            _logger.LogInformation("Card {CardId} completed updating", existingCard.CardId);
        }

        public void DeleteCard(Card card)
        {          
            _context.Cards.Remove(card);
            _context.SaveChanges();
        }

        public Card? FindCard(int cardId)
        {
            var card = _context.Cards
                        .Include(c => c.Answers)
                        .FirstOrDefault(c => c.CardId == cardId);

            return card;
        }

        public List<CardDto> GetCardsByStackId(int stackId)
        {
            return _context.Cards
                .Where(c => c.StackId == stackId)
                .Select(c => new CardDto
                {
                    CardId = c.CardId,
                    Question = c.Question,
                    Answers = c.Answers.Select(a => new AnswerDto
                    {
                        AnswerId = a.AnswerId,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList(),
                    StackId = c.StackId
                })
                .ToList();
        }
    }
}
