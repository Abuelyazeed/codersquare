using codersquare.DAL;

namespace codersquare.BL;

public class LikeReadDto
{
    public string UserId { get; set; }
    public Guid PostId { get; set; }
}