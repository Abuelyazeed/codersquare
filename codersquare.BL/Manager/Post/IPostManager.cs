using codersquare.DAL;

namespace codersquare.BL;

public interface IPostManager
{
    Task<List<Post>> GetAllPosts();
    
    Task CreatePost(CreatePostDto post);
    
    Task<Post?> GetPostById(Guid id);
    
    void DeletePost(Post post);
}