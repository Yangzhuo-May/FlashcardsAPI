using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }


        public string Question { get; set; }
        public string[] Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public int StackId { get; set; }
    }
}
