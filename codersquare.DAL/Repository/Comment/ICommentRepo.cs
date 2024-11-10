namespace codersquare.DAL;

public interface ICommentRepo
{
    Task CreateComment(Comment comment);
    
    Task<int> CountComment(Guid postId);
    
    Task<List<Comment>> GetComments(Guid postId);
    
    Task<Comment> GetCommentById(Guid id);
    
    void DeleteComment(Comment comment);
    
    Task<int> SaveChanges();
}