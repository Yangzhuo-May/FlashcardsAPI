using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface ICardRepository
    {
        void InsertQuestion(Card card);
        void UpdateQuestion(int id, Card card);
        void DeleteQuestion(int cardId);
        Card FindQuestion(int cardId);
        List<Card> GetAllCards();
        List<Card> GetCardsByStackId(int stackId);
    }
}
