using codersquare.DAL;

namespace codersquare.BL;

public interface IUserManager
{
    Task SignUp(SignUp toCreate);
    Task<NewUserDto> Login(LoginDto toLogin);
    
    Task<bool> UpdateUser(UserUpdateDto userToUpdate, string userId);
    
    Task<UserReadDto> GetUserById(string id);
    
    Task<NewUserDto> GetUserByEmail(string email);
    
    Task<NewUserDto> GetUserByUsername(string userName);
}