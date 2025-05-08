using Microsoft.AspNetCore.Identity;

namespace CodeAPI.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
