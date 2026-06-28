using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapCanvo.Core.DTOs;
using CapCanvo.Core.Interfaces;
using CapCanvo.Core.Entities;
using CapCanvo.Apis.Extensions;

namespace CapCanvo.API.Controllers
{
    [ApiController]
    [Route("api/boards")]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IUserService _userService;

        public BoardsController(IBoardService boardService, IUserService userService)
        {
            _boardService = boardService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
        {
            try
            {
                var userId = User.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var user = await _userService.GetByClerkIdAsync(userId);
                if (user is null)
                    return Unauthorized();

                var result = await _boardService.CreateBoardAsync(user.Id, user.DisplayName, request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _boardService.GetMyBoardsAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(string id)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userService.GetByClerkIdAsync(userId);
            if (user is null)
                return Unauthorized();


            var board = await _boardService.GetBoard(id, user.Id);
            if (board is null)
                return NotFound();

            return Ok(board);
        }
    }
}