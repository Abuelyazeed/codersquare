namespace codersquare.BL;

public interface ILikeManager
{
    Task CreateLike(Guid postId,string userId);
    
    Task<bool> DeleteLike( Guid postId, string userId);
    
    Task<int> GetLikesCount(Guid postId);
}