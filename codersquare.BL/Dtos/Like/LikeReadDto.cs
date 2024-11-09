using codersquare.DAL;

namespace codersquare.BL;

public class LikeReadDto
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}