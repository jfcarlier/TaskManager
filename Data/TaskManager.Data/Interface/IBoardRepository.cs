using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;

namespace TaskManager.Data.Interface
{
    public interface IBoardRepository
    {
        IEnumerable<BoardDTO> GetAll();

        BoardDTO GetById(int id);

        BoardDTO Create(BoardDTO board);

        int Delete(int id);
                
        void Update(BoardDTO board);

        int Save();
    }
}
