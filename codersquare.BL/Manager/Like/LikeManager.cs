using codersquare.DAL;

namespace codersquare.BL;

public class LikeManager : ILikeManager
{
    private readonly ILikeRepo _likeRepo;

    public LikeManager(ILikeRepo likeRepo)
    {
        _likeRepo = likeRepo;
    }

    public async Task CreateLike(LikeCreateDto likeToCreate)
    {
        Like like = new Like
        {
            UserId = likeToCreate.UserId,
            PostId = likeToCreate.PostId,
        };
        
        await _likeRepo.CreateLike(like);
        await _likeRepo.SaveChanges(); 
    }

    public async Task<bool> DeleteLike(Guid userId, Guid postId)
    {
        List<Like> likes = await _likeRepo.GetLikes(postId);
        
        Like likeToDelete = likes.FirstOrDefault(l => l.UserId == userId);
        
        if(likeToDelete == null) return false;
        _likeRepo.DeleteLike(likeToDelete);
        await _likeRepo.SaveChanges();
        return true;
    }

    public async Task<List<LikeReadDto>> GetLikes(Guid postId)
    {
        List<Like> likes = await _likeRepo.GetLikes(postId);

        List<LikeReadDto> likesReadDto = likes.Select(like => new LikeReadDto
        {
            UserId = like.UserId,
            PostId = like.PostId
        }).ToList();
        
        return likesReadDto;
    }
}