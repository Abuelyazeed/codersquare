using codersquare.DAL;

namespace codersquare.BL;

public interface IPostManager
{
    Task<List<ReadPostDto>> GetAllPosts();
    
    Task CreatePost(CreatePostDto postToCreate, Guid userId);
    
    Task<ReadPostDto?> GetPostById(Guid id);
    
    void DeletePost(Guid postId);
}