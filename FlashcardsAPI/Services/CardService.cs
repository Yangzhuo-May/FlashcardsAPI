using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;

namespace FlashcardsAPI.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void AddCard(Card card)
        {
            try
            {
                _cardRepository.InsertCard(card);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditCard(int id, CardDto card)
        {
            try
            {
                var cardDb = _cardRepository.FindCard(id);
                if (cardDb == null)
                {
                    throw new Exception("Card not found!!");
                }
                _cardRepository.UpdateCard(cardDb, card);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCard(int id)
        {
            try
            {
                var cardDb = _cardRepository.FindCard(id);
                if (cardDb == null)
                {
                    throw new Exception("Card not found!!");
                }
                _cardRepository.DeleteCard(cardDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Card> GetAllCards()
        {
            try
            {
                var cards = _cardRepository.GetAllCards();
                if (cards == null)
                {
                    throw new Exception("No card be found!!");
                }
                return cards;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Card> GetCardsByStackId(int stackId)
        {
            try
            {
                var cardsByStack = _cardRepository.GetCardsByStackId(stackId);
                if (cardsByStack == null)
                {
                    throw new Exception("No card be found!!");
                }
                return cardsByStack;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
