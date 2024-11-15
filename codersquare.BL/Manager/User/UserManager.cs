using codersquare.DAL;
using Microsoft.AspNetCore.Identity;

namespace codersquare.BL;

public class UserManager : IUserManager
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenManager _tokenManager;

    public UserManager(UserManager<User> userManager, SignInManager<User> signInManager, ITokenManager tokenManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenManager = tokenManager;
    }

    public async Task SignUp(SignUp toCreate)
    {
        User user = new User
        {
            UserName = toCreate.Username,
            Email = toCreate.Email,
            FirstName = toCreate.FirstName,
            LastName = toCreate.LastName
        };
        
        // Create the user with password hashing
        var createdUser = await _userManager.CreateAsync(user, toCreate.Password);
        
        if (!createdUser.Succeeded)
        {
            var errors = string.Join(", ", createdUser.Errors.Select(e => e.Description));
            throw new ArgumentException($"User creation failed: {errors}");
        }
    }

    public async Task<NewUserDto> Login(LoginDto toLogin)
    {
        // Try to find the user by username or email
        var user = await _userManager.FindByNameAsync(toLogin.EmailOrUsername)
                   ?? await _userManager.FindByEmailAsync(toLogin.EmailOrUsername);
        
        // Return null if user is not found
        if (user == null) return null;

        // Check password validity
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, toLogin.Password, false);

        // Return null if password check fails
        if (!signInResult.Succeeded) return null;
        
        return new NewUserDto
        {
            Username = user.UserName,
            Email = user.Email,
            Token = _tokenManager.GenerateToken(user)
        };
    }

    public async Task<bool> UpdateUser(UserUpdateDto userToUpdate, Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return false;
        
        user.FirstName = userToUpdate.FirstName;
        user.LastName = userToUpdate.LastName;
        user.Email = userToUpdate.Email;
        user.UserName = userToUpdate.Username;
        
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<UserReadDto> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return null;

        // Map the User to UserReadDto
        return new UserReadDto
        {
            Username = user.UserName,
            Email = user.Email,
        };
    }

    public async Task<NewUserDto> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user == null ? null : new NewUserDto
        {
            // Id = user.Id,
            // FirstName = user.FirstName,
            // LastName = user.LastName,
            Username = user.UserName,
            Email = user.Email,
        };
    }

    public async Task<NewUserDto> GetUserByUsername(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user == null ? null : new NewUserDto
        {
            // Id = user.Id,
            // FirstName = user.FirstName,
            // LastName = user.LastName,
            Username = user.UserName,
            Email = user.Email,
        };
    }
}