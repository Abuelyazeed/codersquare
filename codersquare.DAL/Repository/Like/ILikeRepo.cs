namespace codersquare.DAL;

public interface ILikeRepo
{
    Task CreateLike(Like like);
    
    void DeleteLike(Like like);
    
    Task<List<Like>> GetLikes(Guid postId);
    
    Task<int> SaveChanges();
}