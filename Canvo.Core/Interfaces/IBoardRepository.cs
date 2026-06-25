using CapCanvo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapCanvo.Core.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> CreateAsync(Board board);
        Task<Board?> GetByIdAsync(string id);
        Task<List<Board>> GetByOwnerIdAsync(string ownerId);
    }
}

