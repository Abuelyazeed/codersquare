namespace codersquare.DAL;

public interface ILikeRepo
{
    Task CreateLike(Like like);
    
    Task<bool> DeleteLike(Guid postId, Guid userId);
    
    Task<int> CountLikes(Guid postId);
    
    Task<int> SaveChanges();
}