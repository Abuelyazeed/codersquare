namespace codersquare.DAL;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime PostedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    
    //Navigation Property
    public User User { get; set; }
    public Post Post { get; set; }
}