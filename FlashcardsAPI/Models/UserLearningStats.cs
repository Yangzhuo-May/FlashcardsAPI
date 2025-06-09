using System.ComponentModel.DataAnnotations;

namespace FlashcardsAPI.Models
{
    public class UserLearningStats
    {
        [Key]
        public int UserId { get; set; }

        public DateTime LastStudyTime { get; set; }
        public User User { get; set; }
    }
}
