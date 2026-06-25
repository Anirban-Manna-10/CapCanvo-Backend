using CapCanvo.Core.Common;

namespace CapCanvo.Core.Entities;
public class CanvasObject : IdModel
{
    public string BoardId { get; set; } = string.Empty; 
    public string Type { get; set; } = string.Empty; // path | rect | text | sticky
    public Dictionary<string, object> Data { get; set; } = new();
    public string CreatedBy { get; set; } = string.Empty;
    public int Version { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}