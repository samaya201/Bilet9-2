using Bilet9.Models.Common;

namespace Bilet9.Models;

public class Chef:BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public Position Position { get; set; }
}
