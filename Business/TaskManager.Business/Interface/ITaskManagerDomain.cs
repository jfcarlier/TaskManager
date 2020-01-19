using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Business.Interface
{
    public interface ITaskManagerDomain
    {
        Task<IEnumerable<BoardDTO>> GetAllBoards();
        Task<BoardDTO> GetBoardById(int id);
        Task<BoardDTO> CreateBoard(BoardDTO board);
        Task<int> UpdateBoard(BoardDTO board);

        Task<IEnumerable<SectionDTO>> GetAllSections();
        Task<SectionDTO> GetSectionById(int id);

        Task<IEnumerable<TaskDTO>> GetAllTasks();
        Task<TaskDTO> GetTaskById(int id);
        Task<TaskDTO> CreateTask(int idBoard, TaskDTO task);
        Task<int> UpdateTask(TaskDTO task);
        Task<int> DeleteTask(int id);
    }
}
