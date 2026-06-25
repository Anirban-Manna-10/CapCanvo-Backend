using MongoDB.Driver;
using CapCanvo.Core.Entities;
using CapCanvo.Core.Interfaces;
using CapCanvo.Infrastructure.Persistence;

namespace CapCanvo.Infrastructure.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly MongoDbContext _context;

        public BoardRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Board> CreateAsync(Board board)
        {
            await _context.Boards.InsertOneAsync(board);
            return board;
        }

        public async Task<Board?> GetByIdAsync(string id)
        {
            return await _context.Boards.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Board>> GetByOwnerIdAsync(string ownerId)
        {
            return await _context.Boards
                .Find(b => b.OwnerId == ownerId)
                .SortByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
    }
}