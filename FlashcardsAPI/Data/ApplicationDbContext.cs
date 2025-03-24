using Microsoft.EntityFrameworkCore;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {   
        }

        public DbSet<Question> Questions { get; set; }
    }
}
