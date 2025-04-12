using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class Stack
    {
        [Key]
        public int StackId { get; set; }

        public required string StackName { get; set; }
    }
}
