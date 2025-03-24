using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public void UpdateQuestion(int id, Question question)
        {
            var questionDb = FindQuestion(id);
            if (questionDb != null)
            {
                questionDb.Problem = question.Problem;
                questionDb.Answer = question.Answer;
                _context.SaveChanges();
            }
        }

        public void DeleteQuestion(int id)
        {
            var questionDb = FindQuestion(id);
            if (questionDb != null)
            {
                _context.Questions.Remove(questionDb);
                _context.SaveChanges();
            }
        }

        public Question FindQuestion(int questionId)
        {
            Question questionDb;
            return questionDb = _context.Questions.Find(questionId);
        }
    }
}
