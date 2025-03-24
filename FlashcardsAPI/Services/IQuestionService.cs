using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IQuestionService
    {
        void AddCard(Question question);
        void EditCard(int id, Question question);
        void DeleteCard(int questionId);
    }
}
