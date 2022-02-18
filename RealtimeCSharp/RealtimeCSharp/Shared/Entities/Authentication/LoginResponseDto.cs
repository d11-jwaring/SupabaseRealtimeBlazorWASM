namespace RealtimeCSharp.Shared.Entities.Authentication
{
    public class LoginResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
