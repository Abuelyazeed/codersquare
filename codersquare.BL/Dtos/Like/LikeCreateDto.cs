using System.ComponentModel.DataAnnotations;

namespace codersquare.BL;

public class LikeCreateDto
{
    [Required]
    public Guid UserId { get; set; }
    
}