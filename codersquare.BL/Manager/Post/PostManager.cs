using codersquare.DAL;

namespace codersquare.BL;

public class PostManager : IPostManager
{
    
    private readonly IPostRepo _postRepo;

    public PostManager(IPostRepo postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<List<PostReadDto>> GetAllPosts()
    {
        List<Post> posts = await _postRepo.GetAllPosts();

        List<PostReadDto> postDtos = posts.Select(post => new PostReadDto
        {
            Id = post.Id,
            Title = post.Title,
            Url = post.Url,
            PostedAt = post.PostedAt,
            UserId = post.UserId,
            Comments = post.Comments.Select(c => new CommentReadDto
            {
                Id = c.Id,
                Content = c.Content,
                PostedAt = c.PostedAt,
                UserId = c.UserId,
            }).ToList(),
            Likes = post.Likes.Select(l => new LikeReadDto
            {
                UserId = l.UserId,
                PostId = l.PostId,
            }).ToList()
        }).ToList();
        
        return postDtos;
    }

    public async Task CreatePost(PostCreateDto postToCreate, string userId)
    {
        Post post = new Post
        {
            Id = Guid.NewGuid(),
            Title = postToCreate.Title,
            Url = postToCreate.Url,
            PostedAt = DateTime.Now,
            UserId = userId
        };
        
        await _postRepo.CreatePost(post);
        await _postRepo.SaveChanges();
    }

    public async Task<PostReadDto?> GetPostById(Guid id)
    {
        Post? post = await _postRepo.GetPostById(id);
        if (post == null) return null;

        return new PostReadDto()
        {
            Id = post.Id,
            Title = post.Title,
            Url = post.Url,
            PostedAt = post.PostedAt,
            UserId = post.UserId,
            Comments = post.Comments.Select(c => new CommentReadDto
            {
                Id = c.Id,
                Content = c.Content,
                PostedAt = c.PostedAt,
                UserId = c.UserId,
            }).ToList(),
            Likes = post.Likes.Select(l => new LikeReadDto
            {
                UserId = l.UserId,
                PostId = l.PostId,
            }).ToList()

        };

    }

    public async Task<bool> DeletePost(Guid postId, string userId)
    {
        
        Post? post = await _postRepo.GetPostById(postId);
        if(post == null) return false;
        
        // Check if the current user is the owner of the post
        if (post.UserId != userId)
        {
            return false; // User is not authorized to delete the post
        }
        _postRepo.DeletePost(post);
        await _postRepo.SaveChanges();
        return true;
    }
}