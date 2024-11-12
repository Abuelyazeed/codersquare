using System.ComponentModel.DataAnnotations;

namespace codersquare.BL;

public class UserSignInDto
{
    [Required(ErrorMessage = "Email or Username is required.")]
    public string EmailOrUsername { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    public String Password { get; set; }
}