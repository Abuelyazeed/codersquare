namespace codersquare.BL;

public class CommentReadDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime PostedAt { get; set; }
    public Guid UserId { get; set; }
}