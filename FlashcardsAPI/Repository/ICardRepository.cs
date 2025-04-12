using System;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface ICardRepository
    {
        void InsertCard(Card card);
        void UpdateCard(Card cardToUpdate, CardDto updatedCard);
        void DeleteCard(Card card);
        Card FindCard(int cardId);
        List<Card> GetAllCards();
        List<Card> GetCardsByStackId(int stackId);
    }
}
