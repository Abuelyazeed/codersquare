using codersquare.DAL;

namespace codersquare.BL;

public class PostReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public DateTime PostedAt { get; set; }
   
    public Guid UserId { get; set; } 
    public List<CommentReadDto> Comments { get; set; } = new List<CommentReadDto>();
    public List<LikeReadDto> Likes { get; set; } = new List<LikeReadDto>();
}