using FlashcardsAPI.Dtos;
using FlashcardsAPI.Models;

namespace FlashcardsAPI.Services
{
    public interface IAuthService
    {
        void AddUser(RegisterRequestDto registerRequer);
        void UpdateUser(RegisterRequestDto registerRequest);
        User FindUser(string username);
        LoginResponseDto CheckUser(RegisterRequestDto registerRequest);
        string GenerateJwtToken(RegisterRequestDto registerRequest);
    }
}
