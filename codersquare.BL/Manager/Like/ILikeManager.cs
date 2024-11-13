namespace codersquare.BL;

public interface ILikeManager
{
    Task CreateLike(Guid postId,LikeCreateDto likeToCreate);
    
    Task<bool> DeleteLike( Guid postId, Guid userId);
    
    Task<int> GetLikesCount(Guid postId);
}