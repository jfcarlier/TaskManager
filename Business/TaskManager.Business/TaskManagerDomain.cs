﻿using System;
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
        public BoardDTO CreateBoard(BoardDTO board)
        {
            var boardDb = boardRepository.Create(board);

            sectionRepository.Create(new SectionDTO()
            {
                Name = "Todo",
                BoardId = boardDb.Id
            });
            sectionRepository.Create(new SectionDTO()
            {
                Name = "Doing",
                BoardId = boardDb.Id
            });
            sectionRepository.Create(new SectionDTO()
            {
                Name = "Done",
                BoardId = boardDb.Id
            });

            return boardDb;
        }
        public int UpdateBoard(BoardDTO board)
        {
            return boardRepository.Update(board);
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

        public IEnumerable<TaskDTO> GetAllTasks() => taskRepository.GetAll();
        public TaskDTO GetTaskById(int id) => taskRepository.GetById(id);
        public TaskDTO CreateTask(int idBoard, TaskDTO task)
        {
            var sectionTodo = sectionRepository.GetAll().Where(id => id.BoardId == idBoard).FirstOrDefault(n => n.Name == "Todo");
            if(sectionTodo == null)
            {
                return null;
            }

            task.SectionId = sectionTodo.Id;
            return taskRepository.Create(task);
        }
        public int UpdateTask(TaskDTO task)
        {
            return taskRepository.Update(task);
        }
        public int DeleteTask(int id)
        {
            return taskRepository.Delete(id);
        }

        public IEnumerable<SectionDTO> GetAllSections()
        {
            return sectionRepository.GetAll();
        }
        public SectionDTO GetSectionById(int id)
        {
            return sectionRepository.GetById(id);
        }
    }
}
