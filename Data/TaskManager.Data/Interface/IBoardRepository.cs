using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Data.Interface
{
    public interface IBoardRepository
    {
        Task<IEnumerable<BoardDTO>> GetAll();

        Task<BoardDTO> GetById(int id);

        Task<BoardDTO> Create(BoardDTO board);

        Task<int> Delete(int id);
                
        Task<int> Update(BoardDTO board);

        Task<int> Save();
    }
}
