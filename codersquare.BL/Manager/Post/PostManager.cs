using codersquare.DAL;

namespace codersquare.BL;

public class PostManager : IPostManager
{
    
    private readonly IPostRepo _postRepo;

    public PostManager(IPostRepo postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<List<ReadPostDto>> GetAllPosts()
    {
        List<Post> posts = await _postRepo.GetAllPosts();

        List<ReadPostDto> postDtos = posts.Select(post => new ReadPostDto
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

    public async Task CreatePost(CreatePostDto postToCreate, Guid userId)
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

    public async Task<ReadPostDto?> GetPostById(Guid id)
    {
        Post post = await _postRepo.GetPostById(id);
        if (post == null) return null;

        return new ReadPostDto()
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

    public async void DeletePost(Guid postId)
    {
        throw new NotImplementedException();
    }
}