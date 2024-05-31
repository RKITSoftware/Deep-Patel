using Mail_API.Dtos;
using Mail_API.Models;

namespace Mail_API.Interface
{
    public interface IUserRepository
    {
        bool IsEmailExist(string email);
        bool IsUserExist(LoginUserDto loginUserDto);
        bool IsUserNameExist(string userName);
        Response LoginUser(LoginUserDto loginUserDto);
        Response RegisterUser(User user);
        bool SaveChanges();
    }
}
