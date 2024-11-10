namespace codersquare.BL;

public interface IUserManager
{
    Task CreateUser(UserCreateDto userToCreate);
    
    Task<UserReadDto> GetUserById(Guid id);
    
    Task<UserReadDto> GetUserByEmail(string email);
    
    Task<UserReadDto> GetUserByUsername(string userName);
}