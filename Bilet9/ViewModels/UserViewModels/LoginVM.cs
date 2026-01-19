using System.ComponentModel.DataAnnotations;

namespace Bilet9.ViewModels.UserViewModels;

public class LoginVM
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
