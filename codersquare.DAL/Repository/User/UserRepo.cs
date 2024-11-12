using Microsoft.EntityFrameworkCore;

namespace codersquare.DAL;

public class UserRepo : IUserRepo
{
    private readonly CodersquareContext _context;
    
    public UserRepo(CodersquareContext context)
    {
        _context = context;
    }
    
    public async Task SignUp(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetUserByUsername(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
    }

    public void UpdateUser(User user)
    {
        //throw new NotImplementedException();
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}