using codersquare.DAL;

namespace codersquare.BL;

public class UserManager : IUserManager
{
    private readonly IUserRepo _userRepo;

    public UserManager(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task CreateUser(UserCreateDto userToCreate)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = userToCreate.FirstName,
            LastName = userToCreate.LastName,
            Email = userToCreate.Email,
            Password = userToCreate.Password,
            Username = userToCreate.Username,
        };
        
        await _userRepo.CreateUser(user);
        await _userRepo.SaveChanges();
    }

    public async Task<UserReadDto> GetUserById(Guid id)
    {
        User user = await _userRepo.GetUserById(id);
        if (user == null) return null;

        return new UserReadDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
        };
    }

    public async Task<UserReadDto> GetUserByEmail(string email)
    {
        User user = await _userRepo.GetUserByEmail(email);
        if (user == null) return null;

        return new UserReadDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
        };
    }

    public async Task<UserReadDto> GetUserByUsername(string userName)
    {
        User user = await _userRepo.GetUserByUsername(userName);
        if (user == null) return null;

        return new UserReadDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
        };
    }
}