using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
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
        }
    }
}
