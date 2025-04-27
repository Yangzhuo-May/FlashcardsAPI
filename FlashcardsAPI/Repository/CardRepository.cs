using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using FlashcardsAPI.Dtos;

namespace FlashcardsAPI.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertCard(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }

        public void UpdateCard(Card cardToUpdate, CardDto updatedCard)
        {
            cardToUpdate.Question = updatedCard.Question;
            cardToUpdate.Answers = updatedCard.Answers;
            cardToUpdate.CorrectAnswer = updatedCard.CorrectAnswer;
            cardToUpdate.StackId = updatedCard.StackId;
            _context.SaveChanges();
        }

        public void DeleteCard(Card card)
        {          
            _context.Cards.Remove(card);
            _context.SaveChanges();
        }

        public Card? FindCard(int cardId)
        {
            return _context.Cards.Find(cardId) ?? null;
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
