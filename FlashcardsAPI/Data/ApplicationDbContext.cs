using Microsoft.EntityFrameworkCore;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {   

        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<User> Users {  get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<FavoriteStack> FavoriteStacks { get; set; }
        public DbSet<UserLearningStats> UserLearningStats { get; set; }
        public DbSet<StackLearningStats> StackLearningStats { get; set; }
        public DbSet<AnswerRecord> AnswerRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.Stack)
                .WithMany(s => s.Cards)
                .HasForeignKey(c => c.StackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stack>()
                .HasOne(s => s.User)
                .WithMany(u => u.Stacks)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Card)
                .WithMany(c => c.Answers)
                .HasForeignKey(a => a.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteStack>()
                .HasIndex(f => new { f.UserId, f.StackId })
                .IsUnique();
        }
    }
}
