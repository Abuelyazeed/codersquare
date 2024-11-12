using codersquare.DAL;

namespace codersquare.BL;

public class CommentManager : ICommentManager
{
    private readonly ICommentRepo _commentRepo;
    
    public CommentManager(ICommentRepo commentRepo)
    {
        _commentRepo = commentRepo;
    }
    
    public async Task CreateComment(CommentCreateDto commentToCreate, Guid userId, Guid postId)
    {
        Comment comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = commentToCreate.Content,
            PostedAt = DateTime.Now,
            UserId = userId,
            PostId = postId   
        };

        await _commentRepo.CreateComment(comment);
        await _commentRepo.SaveChanges();
    }
    
    public async Task<int> CountComment(Guid postId)
    {
        return await _commentRepo.CountComment(postId);  
    }
    
    public async Task<List<CommentReadDto>> GetComments(Guid postId)
    {
        List<Comment> comments = await _commentRepo.GetComments(postId);

        List<CommentReadDto> commentDtos = comments.Select(c => new CommentReadDto
        {
            Id = c.Id,
            Content = c.Content,
            PostedAt = c.PostedAt,
            UserId = c.UserId
        }).ToList();

        return commentDtos;
    }
    
    public async void DeleteComment(Guid id)
    {
        Comment comment = await _commentRepo.GetCommentById(id);
        
        _commentRepo.DeleteComment(comment);
        await _commentRepo.SaveChanges();
        
    }
}
