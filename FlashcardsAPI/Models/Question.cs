using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Problem { get; set; }
        public string Answers { get; set; }

        public Question()
        {
           
        }
    }
}
