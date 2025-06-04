using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;

namespace FlashcardsAPI.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IStackRepository _stackRepository;

        public CardService(ICardRepository cardRepository, IStackRepository stackRepository)
        {
            _stackRepository = stackRepository;
            _cardRepository = cardRepository;
        }

        public void AddCard(CardDto card, int userId)
        {
            try
            {
                var stack = _stackRepository.FindStack(card.StackId);

                if (stack == null)
                {
                    throw new Exception($"Stack with ID {card.StackId} not found.");
                }

                Card newCard = new Card
                {
                    UserId = userId,
                    Question = card.Question,
                    Answers = card.Answers,
                    CorrectAnswer = card.CorrectAnswer,
                    StackId = card.StackId,
                    Stack = stack
                };

                _cardRepository.InsertCard(newCard);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddMultiCard(BulkImportRequest request, int userId)
        {
            try
            {
                var newCards = new List<Card>();

                foreach (var cardDto in request.Cards)
                {
                    var stack = _stackRepository.FindStack(cardDto.StackId);
                    if (stack == null)
                        throw new Exception($"Stack with ID {cardDto.StackId} not found.");

                    newCards.Add(new Card
                    {
                        UserId = userId,
                        Question = cardDto.Question,
                        Answers = cardDto.Answers,
                        CorrectAnswer = cardDto.CorrectAnswer,
                        StackId = cardDto.StackId,
                        Stack = stack
                    });
                }

                _cardRepository.InsertCards(newCards);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditCard(CardDto card)
        {
            try
            {
                var cardDb = _cardRepository.FindCard(card.CardId);
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

        public List<Card> GetAllCards(int userId)
        {
            try
            {
                var cards = _cardRepository.GetAllCards(userId);
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
