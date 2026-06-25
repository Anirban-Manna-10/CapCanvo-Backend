using CapCanvo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapCanvo.Core.Interfaces
{
    public interface IBoardService
    {
        Task<BoardResponse> CreateBoardAsync(string ownerId, string ownerName, CreateBoardRequest request);
        Task<List<BoardResponse>> GetMyBoardsAsync(string userId);
    }
}
