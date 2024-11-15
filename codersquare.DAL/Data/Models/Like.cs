namespace codersquare.DAL;

public class Like
{
    public string UserId { get; set; }
    public Guid PostId { get; set; }
    
    //Navigation Property
    public User User { get; set; }
    public Post Post { get; set; }
}