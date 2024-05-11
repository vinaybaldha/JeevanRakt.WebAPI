using JeevanRakt.Core.Domain.Entities;

namespace JeevanRakt.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? EmployeeName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber {  get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? Role {  get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

        public string? FilePath { get; set; }

        public Guid UserId { get; set; }
        public Guid? BloodBankId { get; set; }
    }
}
