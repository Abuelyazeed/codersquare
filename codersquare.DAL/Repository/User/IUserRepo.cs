namespace codersquare.DAL;

public interface IUserRepo
{
    Task CreateUser(User user);
    
    Task<User> GetUserById(Guid id);
    
    Task<User> GetUserByEmail(string email);
    
    Task<User> GetUserByUsername(string userName);
    Task<int> SaveChanges();
}