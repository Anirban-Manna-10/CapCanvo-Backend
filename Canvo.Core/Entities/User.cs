using CapCanvo.Core.Common;

namespace CapCanvo.Core.Entities;

public class User  : IdModel
{
    public string ClerkId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}