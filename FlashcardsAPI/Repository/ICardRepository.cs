using FlashcardsAPI.Models;

namespace FlashcardsAPI.Repository
{
    public interface ICardRepository
    {
        void InsertQuestion(Question question);
        void UpdateQuestion(int id, Question question);
        void DeleteQuestion(int questionId);
        Question FindQuestion(int questionId);
    }
}
