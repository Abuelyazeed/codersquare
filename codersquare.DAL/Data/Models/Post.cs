namespace codersquare.DAL;

public class Post
{
    
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Url { get; set; }
    public DateTime PostedAt { get; set; }
    public string UserId  { get; set; }
    
    //Navigation Property
    public User User { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Like> Likes { get; set; } = new List<Like>();
}