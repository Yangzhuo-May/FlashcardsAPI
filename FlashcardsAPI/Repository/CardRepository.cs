using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
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

        public void InsertCards(List<Card> cards)
        {
            _context.Cards.AddRange(cards);
            _context.SaveChanges();
        }

        public void UpdateCard(Card cardToUpdate, CardDto updatedCard)
        {
            cardToUpdate.Question = updatedCard.Question;
            cardToUpdate.Answers = updatedCard.Answers.Select(a => new Answer
            {
                AnswerText = a.AnswerText,
                IsCorrect = a.IsCorrect
            }).ToList();
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

        public List<Card> GetCardsByStackId(int stackId)
        {
            return _context.Cards.Where(c => c.StackId == stackId).ToList();
        }
    }
}
