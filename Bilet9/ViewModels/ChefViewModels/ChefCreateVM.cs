using System.ComponentModel.DataAnnotations;

namespace Bilet9.ViewModels.ChefViewModels;

public class ChefCreateVM
{
    [Required,MinLength(3)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public IFormFile Image { get; set; }
    [Required]
    public int PositionId { get; set; } 
}
