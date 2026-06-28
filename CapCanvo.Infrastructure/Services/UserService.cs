// CapCanvo.Infrastructure/Services/UserService.cs
using CapCanvo.Core.DTOs;
using CapCanvo.Core.Entities;
using CapCanvo.Core.Interfaces;
using CapCanvo.Infrastructure.Persistence;
using MongoDB.Driver;
using System.Security.Claims;

namespace CapCanvo.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly MongoDbContext _context;

    public UserService(MongoDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetAsync(string id)
    {
        return await _context.Users.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    public async Task<User> GetByClerkIdAsync(string clerkId) =>
        await _context.Users.Find(u => u.ClerkId == clerkId).FirstOrDefaultAsync();

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.InsertOneAsync(user);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
    }

    public async Task<UserResponse> SyncUserAsync(string clerkId, SyncUserRequest request)
    {
        var existing = await GetByClerkIdAsync(clerkId);

        if (existing != null)
        {
            // keep Mongo in sync with whatever changed on Clerk's side
            existing.Email = request.Email;
            existing.DisplayName = request.DisplayName;
            existing.AvatarUrl = request.AvatarUrl;

            await UpdateAsync(existing);
            return ToResponse(existing);
        }

        var user = new User
        {
            ClerkId = clerkId,
            Email = request.Email,
            DisplayName = request.DisplayName,
            AvatarUrl = request.AvatarUrl,
            CreatedAt = DateTime.UtcNow
        };

        await CreateAsync(user);
        return ToResponse(user);
    }

    private static UserResponse ToResponse(User user) => new()
    {
        Id = user.Id,
        ClerkId = user.ClerkId,
        Email = user.Email,
        DisplayName = user.DisplayName,
        AvatarUrl = user.AvatarUrl,
        CreatedAt = user.CreatedAt
    };

    public async Task<User> GetCurrentUser(ClaimsPrincipal User)
    {
        User res = new();
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
            return res;

        return await GetByClerkIdAsync(userId);
    }
}
