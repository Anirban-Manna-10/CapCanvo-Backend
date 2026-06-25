using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapCanvo.Core.DTOs;
using CapCanvo.Core.Interfaces;

namespace CapCanvo.API.Controllers
{
    [ApiController]
    [Route("api/boards")]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // NOTE: ownerName comes from the JWT's name-ish claim if present;
            // Clerk's default token may not include a display name claim at all —
            // see note below this code block.
            var ownerName = User.FindFirst("name")?.Value ?? "Unknown";

            try
            {
                var result = await _boardService.CreateBoardAsync(userId, ownerName, request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBoards()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _boardService.GetMyBoardsAsync(userId);
            return Ok(result);
        }
    }
}