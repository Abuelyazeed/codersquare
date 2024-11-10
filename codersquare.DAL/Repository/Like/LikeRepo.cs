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

    public void DeleteLike(Like like)
    {
         _context.Likes.Remove(like);
    }

    public async Task<List<Like>> GetLikes(Guid postId)
    {
        return await _context.Likes.Where(x => x.PostId == postId).ToListAsync();
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}