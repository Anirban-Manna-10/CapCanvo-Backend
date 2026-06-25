using CapCanvo.Core.DTOs;
using CapCanvo.Core.Entities;
using CapCanvo.Core.Enums;
using CapCanvo.Core.Interfaces;
using CapCanvo.Infrastructure.Repositories;

namespace CapCanvo.Infrastructure.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IBoardMemberRepository _boardMemberRepository;

        public BoardService(
            IBoardRepository boardRepository,
            IBoardMemberRepository boardMemberRepository)
        {
            _boardRepository = boardRepository;
            _boardMemberRepository = boardMemberRepository;
        }

        public async Task<BoardResponse> CreateBoardAsync(
            string ownerId, string ownerName, CreateBoardRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Board title is required.");

            var board = new Board
            {
                Title = request.Title.Trim(),
                OwnerId = ownerId,
                OwnerName = ownerName
            };

            await _boardRepository.CreateAsync(board);

            var ownerMember = new BoardMember
            {
                BoardId = board.Id,
                UserId = ownerId,
                UserName = ownerName,
                Role = BoardRole.Owner
            };

            await _boardMemberRepository.CreateAsync(ownerMember);

            return ToResponse(board);
        }

        public async Task<List<BoardResponse>> GetMyBoardsAsync(string userId)
        {
            // boards I own, directly
            var owned = await _boardRepository.GetByOwnerIdAsync(userId);

            // boards I'm a member of (covers Editor/Viewer; Owner rows also exist here
            // but we dedupe by board Id below so owned boards aren't doubled)
            var memberships = await _boardMemberRepository.GetByUserIdAsync(userId);
            var memberBoardIds = memberships.Select(m => m.BoardId).ToHashSet();

            var ownedIds = owned.Select(b => b.Id).ToHashSet();
            var missingIds = memberBoardIds.Except(ownedIds);

            var result = owned.Select(ToResponse).ToList();

            foreach (var id in missingIds)
            {
                var board = await _boardRepository.GetByIdAsync(id);
                if (board != null) result.Add(ToResponse(board));
            }

            return result.OrderByDescending(b => b.CreatedAt).ToList();
        }

        private static BoardResponse ToResponse(Board board) => new()
        {
            Id = board.Id,
            Title = board.Title,
            OwnerId = board.OwnerId,
            OwnerName = board.OwnerName,
            ShareToken = board.ShareToken,
            CreatedAt = board.CreatedAt,
            UpdatedAt = board.UpdatedAt
        };
    }
}