using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface ICardService
    {
        void AddCard(CardDto card, int userId);
        void EditCard(int id, CardDto card);
        void DeleteCard(int cardId);
        List<Card> GetAllCards(int userId);
        List<Card> GetCardsByStackId(int stackId);
    }
}
