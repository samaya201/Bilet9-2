using Microsoft.AspNetCore.Identity;

namespace Bilet9.Models;

public class AppUser:IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
