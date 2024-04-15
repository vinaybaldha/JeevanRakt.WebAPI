using JeevanRakt.Core.Domain.Identity;
using JeevanRakt.Core.DTO;

namespace JeevanRakt.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user, List<string> role);
    }
}
