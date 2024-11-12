using codersquare.DAL;

namespace codersquare.BL;

public class UserManager : IUserManager
{
    private readonly IUserRepo _userRepo;

    public UserManager(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task SignUp(UserCreateDto userToCreate)
    {
        bool usernameExists = await _userRepo.GetUserByUsername(userToCreate.Username) != null;
        bool emailExists = await _userRepo.GetUserByEmail(userToCreate.Email) != null;
        
        if (usernameExists || emailExists)
        {
            throw new Exception("Username or email already exists.");
        }
        
        User user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = userToCreate.FirstName,
            LastName = userToCreate.LastName,
            Email = userToCreate.Email,
            Password = userToCreate.Password,
            Username = userToCreate.Username,
        };
        
        await _userRepo.SignUp(user);
        await _userRepo.SaveChanges();
    }

    public async Task<UserReadDto> SignIn(UserSignInDto userToSignIn)
    {
        // Try to find the user by username or email
        var user = await _userRepo.GetUserByUsername(userToSignIn.EmailOrUsername) 
                   ?? await _userRepo.GetUserByEmail(userToSignIn.EmailOrUsername);

        // If user is still null, invalid credentials
        if (user == null || user.Password != userToSignIn.Password)
        {
            throw new Exception("Invalid username/email or password.");
        }
        
        return new UserReadDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
        };
    }

    public async Task<bool> UpdateUser(UserUpdateDto userToUpdate, Guid userId)
    {
        User user = await _userRepo.GetUserById(userId);
        
        user.FirstName = userToUpdate.FirstName;
        user.LastName = userToUpdate.LastName;
        user.Email = userToUpdate.Email;
        user.Password = userToUpdate.Password;
        user.Username = userToUpdate.Username;
        
        int numberOfAffectedRows = await _userRepo.SaveChanges();
        return numberOfAffectedRows > 0;
    }

    public async Task<UserReadDto> GetUserById(Guid id)
    {
        User user = await _userRepo.GetUserById(id);

        return new UserReadDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
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
            Email = user.Email,
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
            Email = user.Email,
        };
    }
}