using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace codersquare.DAL;

public class CodersquareContext : IdentityDbContext<User>
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Like> Likes => Set<Like>();
    public DbSet<Comment> Comments => Set<Comment>();

    public CodersquareContext(DbContextOptions<CodersquareContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define composite primary key for the Like entity
        modelBuilder.Entity<Like>()
            .HasKey(l => new { l.UserId, l.PostId });
        
    }
}