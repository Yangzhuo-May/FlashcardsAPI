using FlashcardsAPI;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashcardsAPI.Models;

namespace WhenCardDataIsCorrect
{
    [TestClass]
    public class CardDataTest
    {
        [TestMethod]
        
        public void CanCreateCardInstance()
        {
            var card = new CardDataTest
            {
                Question = "What is Unit Test?",
                Answers = new[] { "Testing a single unit of code", "Testing an entire application", "Testing a database connection", "Testing "
            }
        }
    }
}
