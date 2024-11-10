using Microsoft.EntityFrameworkCore;

namespace codersquare.DAL;

public class CommentRepo : ICommentRepo
{
    private readonly CodersquareContext _context;

    public CommentRepo(CodersquareContext context)
    {
        _context = context;
    }

    public async Task CreateComment(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public async Task<int> CountComment(Guid postId)
    {
        return await _context.Comments.Where(c => c.PostId == postId).CountAsync();
    }

    public async Task<List<Comment>> GetComments(Guid postId)
    {
        return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
    }

    public async Task<Comment> GetCommentById(Guid id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public void DeleteComment(Comment comment)
    {
        _context.Comments.Remove(comment);
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}