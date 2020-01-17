using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Models;

namespace TaskManager.Data.Provider.Sql.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerContext context;
        private readonly IMapper mapper;

        public TaskRepository(TaskManagerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TaskDTO Create(TaskDTO task)
        {
            if(task == null)
            {
                return null;
            }

            var taskDb = mapper.Map<Task>(task);
            context.Add(taskDb);
            Save();

            return mapper.Map<TaskDTO>(taskDb);
        }

        public int Delete(int id)
        {
            var taskDb = context.Tasks.Find(id);
            context.Tasks.Remove(taskDb);
            return Save();
        }

        public IEnumerable<TaskDTO> GetAll() => mapper.Map<IEnumerable<TaskDTO>>(context.Tasks);

        public TaskDTO GetById(int id) => mapper.Map<TaskDTO>(context.Tasks.Find(id));

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TaskDTO task)
        {
            if (task == null)
            {
                return;
            }

            var taskDb = context.Tasks.Find(task.Id);
            context.Entry(taskDb).CurrentValues.SetValues(mapper.Map<Task>(task));
            Save();
        }
    }
}
