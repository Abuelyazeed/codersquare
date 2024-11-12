using codersquare.DAL;

namespace codersquare.BL;

public interface IPostManager
{
    Task<List<PostReadDto>> GetAllPosts();
    
    Task CreatePost(PostCreateDto toPostCreate);
    
    Task<PostReadDto?> GetPostById(Guid id);
    
    Task<bool> DeletePost(Guid postId);
}