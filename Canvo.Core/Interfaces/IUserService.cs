using CapCanvo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapCanvo.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> SyncUserAsync(string clerkId, SyncUserRequest request);
    }
}
