using Moq;
using FlashcardsAPI.Repository;
using Xunit;
using System.Linq;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using FlashcardsAPI.Models;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;

namespace FlashcardsAPI.Tests.UnitTests
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _mockUserRepository;
        private readonly Mock<IValidator<RegisterRequestDto>> _mockRegisterValidator;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<AuthService>> _mockLogger;
        private readonly AuthService _userService;

        public AuthServiceTests()
        {
            _mockUserRepository = new Mock<IAuthRepository>();
            _mockRegisterValidator = new Mock<IValidator<RegisterRequestDto>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<AuthService>>();

            _mockConfiguration.Setup(config => config["Jwt:Issuer"]).Returns("testissuer");
            _mockConfiguration.Setup(config => config["Jwt:Audience"]).Returns("testaudience");
            _mockConfiguration.Setup(config => config["Jwt:Key"]).Returns("thisisasecretkeyforjsonwebtoken");

            _userService = new AuthService(
           _mockRegisterValidator.Object,
           _mockConfiguration.Object, 
           _mockUserRepository.Object,
           _mockLogger.Object        
       );
        }

        [Fact]
        public void AddUser_ValidRequest_ShouldInsertUser()
        {
            // Arrange
            var registerRequest = new RegisterRequestDto { Username = "testuser", Email = "test@example.com", Password = "Password123!", ConfirmPassword = "Password123!" };
            var validationResult = new ValidationResult();

            _mockRegisterValidator.Setup(v => v.Validate(registerRequest)).Returns(validationResult);
            _mockUserRepository.Setup(repo => repo.EmailExists(registerRequest.Email)).Returns(false);
            _mockUserRepository.Setup(repo => repo.userNameExists(registerRequest.Username)).Returns(false);

            // Act
            _userService.AddUser(registerRequest);

            // Assert
            _mockUserRepository.Verify(repo => repo.InsertUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void AddUser_InvalidRequest_ShouldThrowValidationException()
        {
            // Arrange
            var registerRequest = new RegisterRequestDto { Username = "", Email = "invalid-email", Password = "short", ConfirmPassword = "short" };
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Username", "Username cannot be empty."),
                new ValidationFailure("Email", "Invalid email address.")
            };
            var validationResult = new ValidationResult(validationFailures);
            _mockRegisterValidator.Setup(v => v.Validate(registerRequest)).Returns(validationResult);

            // Act
            Action act = () => _userService.AddUser(registerRequest);

            // Assert
            Assert.Throws<ValidationException>(act);
            var exception = Assert.Throws<ValidationException>(act);
            Assert.Equal(2, exception.Errors.Count());
            _mockUserRepository.Verify(repo => repo.InsertUser(It.IsAny<User>()), Times.Never);
        }


        [Fact]
        public void AddUser_RepeateRequest_ShouldThrowValidationException()
        {
            // Arrange
            var registerRequest = new RegisterRequestDto { Username = "", Email = "invalid-email", Password = "short", ConfirmPassword = "short" };
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Username", "Username cannot be empty."),
                new ValidationFailure("Email", "Invalid email address.")
            };
            var validationResult = new ValidationResult(validationFailures);
            _mockRegisterValidator.Setup(v => v.Validate(registerRequest)).Returns(validationResult);

            // Act
            Action act = () => _userService.AddUser(registerRequest);

            // Assert
            Assert.Throws<ValidationException>(act);
            var exception = Assert.Throws<ValidationException>(act);
            Assert.Equal(2, exception.Errors.Count());
            _mockUserRepository.Verify(repo => repo.InsertUser(It.IsAny<User>()), Times.Never);
        }
    }
}