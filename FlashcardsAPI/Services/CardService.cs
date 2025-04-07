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
                _cardRepository.InsertQuestion(card);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditCard(int id, Card card)
        {
            try
            {
                var questionDb = _cardRepository.FindQuestion(id);
                if (questionDb == null)
                {
                    throw new Exception("Question not found!!");
                }
                _cardRepository.UpdateQuestion(id, card);
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
                var questionDb = _cardRepository.FindQuestion(id);
                if (questionDb == null)
                {
                    throw new Exception("Question not found!!");
                }
                _cardRepository.DeleteQuestion(id);
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
