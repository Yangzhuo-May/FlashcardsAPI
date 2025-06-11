using System.Text.Json;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;

namespace FlashcardsAPI.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IStackRepository _stackRepository;
        private readonly ILogger<CardService> _logger;

        public CardService(ICardRepository cardRepository, IStackRepository stackRepository, ILogger<CardService> logger)
        {
            _stackRepository = stackRepository;
            _cardRepository = cardRepository;
            _logger = logger;
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
                    Question = card.Question,
                    Answers = card.Answers.Select(a => new Answer
                    {
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList(),
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
                        Question = cardDto.Question,
                        Answers = cardDto.Answers.Select(a => new Answer
                        {
                            AnswerText = a.AnswerText,
                            IsCorrect = a.IsCorrect
                        }).ToList(),
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
                _logger.LogInformation("Start Editing，CardId: {CardId}", card.CardId);
                _logger.LogInformation("Start Editing, Card: {card}", JsonSerializer.Serialize(card));

                var cardDb = _cardRepository.FindCard(card.CardId);
                if (cardDb == null)
                {
                    _logger.LogWarning("Didn't find card with CardId: {CardId} ", card.CardId);
                    throw new Exception("Card not found!!");
                }
                _cardRepository.UpdateCard(cardDb, card);
                _logger.LogInformation("Card {CardId} editing completed", card.CardId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error happened when editing card {CardId}", card.CardId);
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

        public List<CardDto> GetAllCards(int userId)
        {
            try
            {
                var stacks = _stackRepository.GetAllStacks(userId).ToList();
                var cards = new List<CardDto>();

                foreach (var item in stacks)
                {
                    var stackCards = _cardRepository.GetCardsByStackId(item.StackId);

                    cards.AddRange(stackCards);
                }

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
        public List<CardDto> GetCardsByStackId(int stackId)
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
