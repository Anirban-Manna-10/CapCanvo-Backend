using CapCanvo.Core.Entities;

namespace CapCanvo.Core.Interfaces
{
    public interface IBoardMemberRepository
    {
        Task<BoardMember> CreateAsync(BoardMember member);
        Task<List<BoardMember>> GetByUserIdAsync(string userId);
        Task<List<BoardMember>> GetByBoardIdAsync(string boardId);
    }
}