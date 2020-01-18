using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Interface;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Repository;
using System.Linq;

namespace TaskManager.Business
{
    public class TaskManagerDomain : ITaskManagerDomain
    {
        private readonly IBoardRepository boardRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly ITaskRepository taskRepository;

        public TaskManagerDomain(
            IBoardRepository boardRepository,
            ISectionRepository sectionRepository,
            ITaskRepository taskRepository)
        {
            this.boardRepository = boardRepository;
            this.sectionRepository = sectionRepository;
            this.taskRepository = taskRepository;
        }

        public IEnumerable<BoardDTO> GetAllBoards() => boardRepository.GetAll();

        public BoardDTO GetBoardById(int id) => boardRepository.GetById(id);

        public IEnumerable<BoardDTO> GetAllBoardsInMemory()
        {
            var temp = boardRepository.GetAll();
            var temp2 = sectionRepository.GetAll();
            foreach(var section in temp)
            {
                
                var temp3 = new List<SectionDTO>();
                foreach (var item in temp2)
                {
                    if(item.BoardId == section.Id)
                    {
                        temp3.Add(item);
                    }
                }
            }
            return temp;
        }

        public BoardDTO CreateBoard(BoardDTO board)
        {                   
            sectionRepository.Create(new SectionDTO()
            {
                Name = "Todo",
                BoardId = board.Id
            });
            sectionRepository.Create(new SectionDTO()
            {
                Name = "Doing",
                BoardId = board.Id
            });
            sectionRepository.Create(new SectionDTO()
            {
                Name = "Done",
                BoardId = board.Id
            });

            return boardRepository.Create(board);
        }

        public void Update(BoardDTO board)
        {
            boardRepository.Update(board);            
        }

        public IEnumerable<SectionDTO> GetAllSections()
        {
            return sectionRepository.GetAll();
        }
    }
}
