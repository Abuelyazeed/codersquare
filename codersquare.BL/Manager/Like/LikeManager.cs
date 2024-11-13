using codersquare.DAL;

namespace codersquare.BL;

public class LikeManager : ILikeManager
{
    private readonly ILikeRepo _likeRepo;

    public LikeManager(ILikeRepo likeRepo)
    {
        _likeRepo = likeRepo;
    }

    public async Task CreateLike(Guid postId,LikeCreateDto likeToCreate)
    {
        Like like = new Like
        {
            UserId = likeToCreate.UserId,
            PostId = postId
        };
        
        await _likeRepo.CreateLike(like);
        await _likeRepo.SaveChanges(); 
    }

    public async Task<bool> DeleteLike(Guid postId, Guid userId)
    {
        bool likeDeleted = await _likeRepo.DeleteLike(postId, userId);
        if (!likeDeleted) return false;

        await _likeRepo.SaveChanges();
        return true;
    }

    public async Task<int> GetLikesCount(Guid postId)
    {
        return await _likeRepo.CountLikes(postId);
    }
}