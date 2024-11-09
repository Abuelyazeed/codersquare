namespace codersquare.DAL;

public class Like
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    
    //Navigation Property
    public User User { get; set; }
    public Post Post { get; set; }
}