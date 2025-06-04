namespace FlashcardsAPI.Dtos
{
    public class LoginResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; } 
        public string? ErrorMessage { get; set; }
    }
}
