using System.ComponentModel.DataAnnotations;

namespace codersquare.BL;

public class CommentCreateDto
{
    [Required]
    [StringLength(500, ErrorMessage = "Comment content must be less than 500 characters.")]
    public string Content { get; set; }
}