using CapCanvo.Core.Entities;

namespace CapCanvo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByClerkIdAsync(string clerkId);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
    }

}
