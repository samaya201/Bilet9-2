using System.ComponentModel.DataAnnotations;

namespace Bilet9.ViewModels.ChefViewModels;

public class ChefUpdateVM
{
    [Required]
    public int Id { get; set; }
    [Required, MinLength(3)]
    public string Name { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    [Required]
    public int PositionId { get; set; }
}
