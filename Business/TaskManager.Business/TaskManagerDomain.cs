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

        public async Task<IEnumerable<BoardDTO>> GetAllBoards() => await boardRepository.GetAll();
        public async Task<BoardDTO> GetBoardById(int id) => await boardRepository.GetById(id);
        public async Task<BoardDTO> CreateBoard(BoardDTO board)
        {
            var boardDb = await boardRepository.Create(board);

            await sectionRepository.Create(new SectionDTO()
            {
                Name = "Todo",
                BoardId = boardDb.Id
            });
            await sectionRepository.Create(new SectionDTO()
            {
                Name = "Doing",
                BoardId = boardDb.Id
            });
            await sectionRepository.Create(new SectionDTO()
            {
                Name = "Done",
                BoardId = boardDb.Id
            });

            return boardDb;
        }
        public async Task<int> UpdateBoard(BoardDTO board)
        {
            return await boardRepository.Update(board);
        }

        //public IEnumerable<BoardDTO> GetAllBoardsInMemory()
        //{
        //    var temp = boardRepository.GetAll();
        //    var temp2 = sectionRepository.GetAll();
        //    foreach(var section in temp)
        //    {
                
        //        var temp3 = new List<SectionDTO>();
        //        foreach (var item in temp2)
        //        {
        //            if(item.BoardId == section.Id)
        //            {
        //                temp3.Add(item);
        //            }
        //        }
        //    }
        //    return temp;
        //}        

        public async Task<IEnumerable<TaskDTO>> GetAllTasks() => await taskRepository.GetAll();
        public async Task<TaskDTO> GetTaskById(int id) => await taskRepository.GetById(id);
        public async Task<TaskDTO> CreateTask(int idBoard, TaskDTO task)
        {
            var sections = await sectionRepository.GetAll();
            var sectionTodo = sections.Where(id => id.BoardId == idBoard).FirstOrDefault(n => n.Name == "Todo");
            if (sectionTodo == null)
            {
                return null;
            }

            task.SectionId = sectionTodo.Id;
            return await taskRepository.Create(task);
        }
        public async Task<int> UpdateTask(TaskDTO task)
        {
            return await taskRepository.Update(task);
        }
        public async Task<int> DeleteTask(int id)
        {
            return await taskRepository.Delete(id);
        }

        public async Task<IEnumerable<SectionDTO>> GetAllSections()
        {
            return await sectionRepository.GetAll();
        }
        public async Task<SectionDTO> GetSectionById(int id)
        {
            return await sectionRepository.GetById(id);
        }
    }
}
