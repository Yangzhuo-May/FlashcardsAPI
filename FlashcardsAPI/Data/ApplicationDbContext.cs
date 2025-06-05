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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.Stack)
                .WithMany(s => s.Cards)
                .HasForeignKey(c => c.StackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.User)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Stack>()
                .HasOne(s => s.User)
                .WithMany(u => u.Stacks)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
