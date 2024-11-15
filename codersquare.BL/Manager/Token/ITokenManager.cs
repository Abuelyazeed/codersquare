using codersquare.DAL;

namespace codersquare.BL;

public interface ITokenManager
{
    string GenerateToken(User user);
}