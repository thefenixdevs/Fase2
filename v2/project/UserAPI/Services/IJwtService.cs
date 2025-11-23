using UserAPI.Models;

namespace UserAPI.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}
