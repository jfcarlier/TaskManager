using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;

namespace TaskManager.Business.Interface
{
    public interface ITaskManagerDomain
    {
        IEnumerable<BoardDTO> GetAllBoards();
        BoardDTO CreateBoard(BoardDTO board);
        IEnumerable<SectionDTO> GetAllSections();
        BoardDTO GetBoardById(int id);
        public void Update(BoardDTO board);
    }
}
