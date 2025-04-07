using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace FlashcardsAPI.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertQuestion(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }

        public void UpdateQuestion(int id, Card card)
        {
            var cardDb = FindQuestion(id);
            if (cardDb != null)
            {
                cardDb.Question = card.Question;
                cardDb.Answers = card.Answers;
                cardDb.CorrectAnswer = card.CorrectAnswer;
                _context.SaveChanges();
            }
        }

        public void DeleteQuestion(int id)
        {
            var cardDb = FindQuestion(id);
            if (cardDb != null)
            {
                _context.Cards.Remove(cardDb);
                _context.SaveChanges();
            }
        }

        public Card FindQuestion(int cardId)
        {
            Card cardDb;
            return cardDb = _context.Cards.Find(cardId);
        }

        public List<Card> GetAllCards()
        {
            return _context.Cards.ToList();
        }

        public List<Card> GetCardsByStackId(int stackId)
        {
            return _context.Cards.Where(c => c.StackId == stackId).ToList();
        }
    }
}
