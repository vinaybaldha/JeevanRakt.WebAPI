namespace JeevanRakt.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? EmployeeName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

        public Guid UserId { get; set; }
    }
}
