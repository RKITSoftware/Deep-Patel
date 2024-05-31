using Mail_API.Models;

namespace Mail_API.Interface
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
