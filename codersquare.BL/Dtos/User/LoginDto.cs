using System.ComponentModel.DataAnnotations;

namespace codersquare.BL;

public class LoginDto
{
    [Required(ErrorMessage = "Email or Username is required.")]
    public string EmailOrUsername { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}