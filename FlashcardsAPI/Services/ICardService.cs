using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface ICardService
    {
        void AddCard(Card card);
        void EditCard(int id, Card card);
        void DeleteCard(int cardId);
        List<Card> GetAllCards();
        List<Card> GetCardsByStackId(int stackId);
    }
}
