namespace codersquare.BL;

public interface ILikeManager
{
    Task CreateLike(LikeCreateDto likeToCreate);
    
    Task<bool> DeleteLike(Guid userId, Guid postId);
    
    Task<List<LikeReadDto>> GetLikes(Guid postId);
}