namespace codersquare.DAL;

public interface ILikeRepo
{
    Task CreateLike(Like like);
    
    Task<bool> DeleteLike(Guid postId, string userId);
    
    Task<int> CountLikes(Guid postId);
    
    Task<int> SaveChanges();
}