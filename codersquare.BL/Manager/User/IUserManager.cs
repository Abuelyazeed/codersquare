namespace codersquare.BL;

public interface IUserManager
{
    Task SignUp(UserCreateDto userToCreate);
    Task<UserReadDto> SignIn(UserSignInDto userToSignIn);
    
    Task<bool> UpdateUser(UserUpdateDto userToUpdate, Guid userId);
    
    Task<UserReadDto> GetUserById(Guid id);
    
    Task<UserReadDto> GetUserByEmail(string email);
    
    Task<UserReadDto> GetUserByUsername(string userName);
}