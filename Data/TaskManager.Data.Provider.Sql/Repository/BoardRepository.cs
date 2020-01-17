using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;

namespace TaskManager.Data.Provider.Sql.Repository
{
    public class BoardRepository : IBoardRepository
    {
        public BoardDTO Create(BoardDTO board)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BoardDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public BoardDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(BoardDTO board)
        {
            throw new NotImplementedException();
        }
    }
}
