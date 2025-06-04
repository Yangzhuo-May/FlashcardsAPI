using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface ICardService
    {
        void AddCard(CardDto card, int userId);
        void AddMultiCard(BulkImportRequest request, int userId);
        void EditCard(CardDto card);
        void DeleteCard(int cardId);
        List<Card> GetAllCards(int userId);
        List<Card> GetCardsByStackId(int stackId);
    }
}
