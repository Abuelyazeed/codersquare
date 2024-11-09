using Microsoft.EntityFrameworkCore;

namespace codersquare.DAL;

public class PostRepo : IPostRepo
{
    private readonly CodersquareContext _context;
    public PostRepo(CodersquareContext context)
    {
        _context = context;
    }
    
    public async Task<List<Post>> GetAllPosts()
    {
        return await _context.Posts
            .Include(u => u.User)
            .Include(l => l.Likes)
            .Include(c => c.Comments)
            .ToListAsync();
    }

    public async Task CreatePost(Post post)
    {
        await _context.Posts.AddAsync(post);
    }

    public async Task<Post?> GetPostById(Guid id)
    {
        return await _context.Posts
            .Include(u => u.User)
            .Include(l => l.Likes)
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void DeletePost(Post post)
    {
        _context.Posts.Remove(post);
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}