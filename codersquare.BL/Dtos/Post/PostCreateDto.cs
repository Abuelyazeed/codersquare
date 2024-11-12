using System.ComponentModel.DataAnnotations;

namespace codersquare.BL;

public class PostCreateDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "URL is required.")]
    [Url(ErrorMessage = "Invalid URL format.")]
    public string Url { get; set; }
    
    [Required]
    public Guid UserId  { get; set; }
}