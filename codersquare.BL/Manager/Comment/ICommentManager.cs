namespace codersquare.BL;

public interface ICommentManager
{
    Task CreateComment(CommentCreateDto commentToCreate, string userId, Guid postId);
    
    Task<int> CountComment(Guid postId);
    
    Task<List<CommentReadDto>> GetComments(Guid postId);
    
    void DeleteComment(Guid id);
}