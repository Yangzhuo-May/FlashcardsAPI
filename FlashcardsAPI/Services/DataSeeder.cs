using Bogus;
using FlashcardsAPI.Models;
using FlashcardsAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Services
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(ApplicationDbContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void SeedData(bool clearExistingData = false, int numberOfUsersToSeed = 5, int stacksPerUser = 3, int cardsPerStack = 5, int answersPerCard = 4)
        {
            _logger.LogInformation("Starting data seeding...");

            if (clearExistingData)
            {
                _logger.LogWarning("Clearing existing data before seeding...");
                // Clear in reverse order of dependency
                _context.Answers.RemoveRange(_context.Answers);
                _context.Cards.RemoveRange(_context.Cards);
                _context.Stacks.RemoveRange(_context.Stacks);
                _context.Users.RemoveRange(_context.Users);
                _context.SaveChanges();
                _logger.LogWarning("Existing data cleared.");
            }

            // Seed Users
            if (!_context.Users.Any())
            {
                _logger.LogInformation($"No users found. Seeding {numberOfUsersToSeed} users...");

                var userFaker = new Faker<User>()
                    .RuleFor(u => u.UserId, f => 0) // Let database generate ID, will be updated after SaveChanges
                    .RuleFor(u => u.Username, f => f.Internet.UserName().Replace(".", "").Replace("-", ""))
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Username))
                    .RuleFor(u => u.PasswordHash, f => f.Internet.Password(8)) // For demo purposes, use a simple password hash
                    .RuleFor(u => u.RegistrationDate, f => f.Date.Past(1).ToUniversalTime()); // Registered within the last year

                var users = userFaker.Generate(numberOfUsersToSeed);
                _context.Users.AddRange(users);
                _context.SaveChanges(); // Save users to get their generated IDs
                _logger.LogInformation($"{users.Count} users seeded successfully.");
            }
            else
            {
                _logger.LogInformation("Users already exist. Skipping user seeding.");
            }

            var existingUserIds = _context.Users.Select(u => u.UserId).ToList();

            // Seed Stacks
            if (!_context.Stacks.Any() && existingUserIds.Any())
            {
                _logger.LogInformation($"No stacks found. Seeding {stacksPerUser * existingUserIds.Count} stacks...");

                var stackFaker = new Faker<Stack>()
                    .RuleFor(s => s.StackId, f => 0) // Let database generate ID
                    .RuleFor(s => s.StackName, f => f.Lorem.Sentence(3, 5)) // Random sentence for stack name
                    .RuleFor(s => s.IsProficient, f => f.Random.Bool())
                    .RuleFor(s => s.IsPublic, f => f.Random.Bool())
                    .RuleFor(s => s.FavoriteCount, f => f.Random.Int(0, 100))
                    .RuleFor(s => s.UserId, f => f.PickRandom(existingUserIds)); // Link to an existing user

                var stacks = new List<Stack>();
                foreach (var userId in existingUserIds)
                {
                    stacks.AddRange(stackFaker.Generate(stacksPerUser).Select(s => { s.UserId = userId; return s; }));
                }
                _context.Stacks.AddRange(stacks);
                _context.SaveChanges(); // Save stacks to get their generated IDs
                _logger.LogInformation($"{stacks.Count} stacks seeded successfully.");
            }
            else if (!existingUserIds.Any())
            {
                _logger.LogWarning("No users available to associate with stacks. Skipping stack seeding.");
            }
            else
            {
                _logger.LogInformation("Stacks already exist. Skipping stack seeding.");
            }

            var existingStackIds = _context.Stacks.Select(s => s.StackId).ToList();

            // Seed Cards
            if (!_context.Cards.Any() && existingStackIds.Any())
            {
                _logger.LogInformation($"No cards found. Seeding {cardsPerStack * existingStackIds.Count} cards...");

                var cardFaker = new Faker<Card>()
                    .RuleFor(c => c.CardId, f => 0) // Let database generate ID
                    .RuleFor(c => c.Question, f => f.Lorem.Sentence(5, 10)); // Random question

                var cards = new List<Card>();
                foreach (var stackId in existingStackIds)
                {
                    cards.AddRange(cardFaker.Generate(cardsPerStack).Select(c => { c.StackId = stackId; return c; }));
                }
                _context.Cards.AddRange(cards);
                _context.SaveChanges(); // Save cards to get their generated IDs
                _logger.LogInformation($"{cards.Count} cards seeded successfully.");
            }
            else if (!existingStackIds.Any())
            {
                _logger.LogWarning("No stacks available to associate with cards. Skipping card seeding.");
            }
            else
            {
                _logger.LogInformation("Cards already exist. Skipping card seeding.");
            }

            var existingCardIds = _context.Cards.Select(c => c.CardId).ToList();

            // Seed Answers
            if (!_context.Answers.Any() && existingCardIds.Any())
            {
                _logger.LogInformation($"No answers found. Seeding {answersPerCard * existingCardIds.Count} answers...");

                var answerFaker = new Faker<Answer>()
                    .RuleFor(a => a.AnswerId, f => 0) // Let database generate ID
                    .RuleFor(a => a.AnswerText, f => f.Lorem.Sentence(3, 8)) // Random answer text
                    .RuleFor(a => a.IsCorrect, f => f.Random.Bool()); // Randomly correct or incorrect

                var answers = new List<Answer>();
                foreach (var cardId in existingCardIds)
                {
                    // Ensure at least one correct answer per card
                    answers.Add(new Answer { AnswerId = 0, AnswerText = "Correct Answer " + Guid.NewGuid().ToString("N").Substring(0, 4), IsCorrect = true, CardId = cardId });

                    // Generate remaining answers, potentially incorrect
                    for (int i = 0; i < answersPerCard - 1; i++)
                    {
                        var answer = answerFaker.Generate();
                        answer.CardId = cardId;
                        answers.Add(answer);
                    }
                }
                _context.Answers.AddRange(answers);
                _context.SaveChanges();
                _logger.LogInformation($"{answers.Count} answers seeded successfully.");
            }
            else if (!existingCardIds.Any())
            {
                _logger.LogWarning("No cards available to associate with answers. Skipping answer seeding.");
            }
            else
            {
                _logger.LogInformation("Answers already exist. Skipping answer seeding.");
            }

            _logger.LogInformation("Data seeding completed.");
        }
    }
}