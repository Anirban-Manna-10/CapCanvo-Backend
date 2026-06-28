using CapCanvo.Core.DTOs;
using CapCanvo.Core.Entities;

namespace CapCanvo.Core.Interfaces
{
    public interface IBoardService
    {
        Task<BoardResponse> CreateBoardAsync(string ownerId, string ownerName, CreateBoardRequest request);
        Task<List<BoardResponse>> GetMyBoardsAsync(string userId);
        Task<Board> GetBoard(string id, string userId); 
    }
}
