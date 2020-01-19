using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;

namespace TaskManager.Business.Interface
{
    public interface ITaskManagerDomain
    {
        IEnumerable<BoardDTO> GetAllBoards();
        BoardDTO GetBoardById(int id);
        BoardDTO CreateBoard(BoardDTO board);
        int UpdateBoard(BoardDTO board);

        IEnumerable<SectionDTO> GetAllSections();
        SectionDTO GetSectionById(int id);

        IEnumerable<TaskDTO> GetAllTasks();
        TaskDTO GetTaskById(int id);
        TaskDTO CreateTask(int idBoard, TaskDTO task);
        int UpdateTask(TaskDTO task);
        int DeleteTask(int id);
    }
}
