namespace CapCanvo.Core.DTOs;
public class UserResponse
{
    public string Id { get; set; } = string.Empty;
    public string ClerkId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
