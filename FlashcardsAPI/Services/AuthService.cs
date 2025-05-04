using System.Security.Claims;
using System.Text;
using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FlashcardsAPI.Repository;

namespace FlashcardsAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _userRepository;
        private readonly IValidator<RegisterRequestDto> _registerValidator;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _key;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IValidator<RegisterRequestDto> registerValidator, IConfiguration configuration, IAuthRepository userRepository, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            var keyString = configuration["Jwt:Key"];

            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);
            _key = new SymmetricSecurityKey(keyBytes);

            _registerValidator = registerValidator;
        }

        public void AddUser(RegisterRequestDto registerRequest)
        {
            var validationResult = _registerValidator.Validate(registerRequest);
  
            if (!validationResult.IsValid)
            {
                throw new ValidationException("Validation failed for the register requer.", validationResult.Errors);
            }

            if (_userRepository.EmailExists(registerRequest.Email))
            {
                throw new ApplicationException($"Email '{registerRequest.Email}' already exists.");
            }

            if (_userRepository.userNameExists(registerRequest.Username))
            {
                throw new ApplicationException($"Username '{registerRequest.Username}' already exists.");
            }
 
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
             
            var user = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PasswordHash = hashedPassword,
                RegistrationDate = DateTime.UtcNow,
            };
            
            _userRepository.InsertUser(user);
        }

        public void UpdateUser(RegisterRequestDto registerRequest)
        {
             
        }

        public User FindUser(string emil)
        {
            return _userRepository.FindUser(emil);
        }

        public LoginResponseDto CheckUser(LoginRequestDto loginRequest)
        {
            var userDb = FindUser(loginRequest.Email);

            if (userDb == null) return new LoginResponseDto { IsAuthenticated = false, ErrorMessage = "User does not exist." };

            var token = GenerateJwtToken(loginRequest);

            if (BCrypt.Net.BCrypt.Verify(loginRequest.Password, userDb.PasswordHash))
            {
                return new LoginResponseDto { IsAuthenticated = true, Token = token };
            }
            else
            {
                return new LoginResponseDto { IsAuthenticated = false, ErrorMessage = "User does not exist." };
            }
        }

        public string GenerateJwtToken(LoginRequestDto loginRequest)
        {
            var userDb = FindUser(loginRequest.Email);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDb.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, loginRequest.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
 
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
