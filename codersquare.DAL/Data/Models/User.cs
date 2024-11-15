using Microsoft.AspNetCore.Identity;

namespace codersquare.DAL;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}