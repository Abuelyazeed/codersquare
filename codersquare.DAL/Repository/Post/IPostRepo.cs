namespace codersquare.DAL;


public interface IPostRepo
{
    Task<List<Post>> GetAllPosts();
    
    Task CreatePost(Post post);
    
    Task<Post?> GetPostById(Guid id);
    
    void DeletePost(Post post);
    
    Task<int> SaveChanges();
}