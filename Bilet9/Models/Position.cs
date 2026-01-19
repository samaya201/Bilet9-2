using Bilet9.Models.Common;

namespace Bilet9.Models;

public class Position : BaseEntity 
{
    public string Title { get; set; } = null!;
    public ICollection<Chef> Chefs { get; set; } = [];
}
