// CapCanvo.API/Controllers/UsersController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapCanvo.Core.DTOs;
using CapCanvo.Core.Interfaces;

namespace CapCanvo.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("sync")]
        [Authorize]
        public async Task<IActionResult> SyncUser([FromBody] SyncUserRequest request)
        {
            var clerkId = User.FindFirst("sub")?.Value
            ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(clerkId))
                return Unauthorized();

            var result = await _userService.SyncUserAsync(clerkId, request);
            return Ok(result);
        }
    }
}