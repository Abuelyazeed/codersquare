using Microsoft.EntityFrameworkCore;
namespace codersquare.DAL;

public class LikeRepo : ILikeRepo
{
    private readonly CodersquareContext _context;

    public LikeRepo(CodersquareContext context)
    {
        _context = context;
    }

    public async Task CreateLike(Like like)
    {
        await _context.Likes.AddAsync(like);
    }

    public async Task<bool> DeleteLike(Guid postId, string userId)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
        
        if (like == null)
        {
            Console.WriteLine($"Like not found: PostId={postId}, UserId={userId}");
            return false;
        }
        
        _context.Likes.Remove(like);
        return true;
    }

    public async Task<int> CountLikes(Guid postId)
    {
        return await _context.Likes.CountAsync(l => l.PostId == postId);
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}