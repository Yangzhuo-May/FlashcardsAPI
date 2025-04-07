using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class Stack
    {
        [Key]
        public int StackId { get; set; }

        public string StackName { get; set; }
    }
}
