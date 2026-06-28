using CapCanvo.Core.DTOs;
using CapCanvo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapCanvo.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetAsync(string id);
        Task<UserResponse> SyncUserAsync(string clerkId, SyncUserRequest request);
        Task<User> GetByClerkIdAsync(string clerkId);
        Task<User> GetCurrentUser(ClaimsPrincipal user);
    }
}
