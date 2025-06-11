using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface ICardRepository
    {
        void InsertCard(Card card);
        void InsertCards(List<Card> cards);
        void UpdateCard(Card cardToUpdate, CardDto updatedCard);
        void DeleteCard(Card card);
        Card? FindCard(int cardId);
        List<CardDto> GetCardsByStackId(int stackId);
    }
}
