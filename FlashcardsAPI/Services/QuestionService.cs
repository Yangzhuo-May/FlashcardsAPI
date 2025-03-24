using FlashcardsAPI.Models;
using FlashcardsAPI.Repository;

namespace FlashcardsAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ICardRepository _cardRepository;
        public QuestionService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void AddCard(Question question)
        {
            try
            {
                _cardRepository.InsertQuestion(question);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditCard(int id, Question question)
        {
            try
            {
                var questionDb = _cardRepository.FindQuestion(id);
                if (questionDb == null)
                {
                    throw new Exception("Question not found!!");
                }
                _cardRepository.UpdateQuestion(id, question);
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
    }
}
