using System.ComponentModel.DataAnnotations;

namespace codersquare.BL;

public class SignUp
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")] 
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(30, ErrorMessage = "Username cannot exceed 30 characters.")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    public String Password { get; set; }
}