namespace codersquare.DAL;

public interface ICommentRepo
{
    Task CreateComment(Comment comment);
    
    Task<int> CountComment(Guid postId);
    
    Task<List<Comment>> GetComments(Guid postId);
    
    void DeleteComment(Comment comment);
    
    Task<int> SaveChanges();
}