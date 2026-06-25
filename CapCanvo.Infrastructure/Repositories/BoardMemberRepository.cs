// CapCanvo.Infrastructure/Repositories/BoardMemberRepository.cs
using MongoDB.Driver;
using CapCanvo.Core.Entities;
using CapCanvo.Core.Interfaces;
using CapCanvo.Infrastructure.Persistence;

namespace CapCanvo.Infrastructure.Repositories
{
    public class BoardMemberRepository : IBoardMemberRepository
    {
        private readonly MongoDbContext _context;

        public BoardMemberRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<BoardMember> CreateAsync(BoardMember member)
        {
            await _context.BoardMembers.InsertOneAsync(member);
            return member;
        }

        public async Task<List<BoardMember>> GetByUserIdAsync(string userId)
        {
            return await _context.BoardMembers.Find(m => m.UserId == userId).ToListAsync();
        }

        public async Task<List<BoardMember>> GetByBoardIdAsync(string boardId)
        {
            return await _context.BoardMembers.Find(m => m.BoardId == boardId).ToListAsync();
        }
    }
}