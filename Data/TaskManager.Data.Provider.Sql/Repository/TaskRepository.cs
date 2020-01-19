using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Models;

namespace TaskManager.Data.Provider.Sql.Repository
{
    //CRUD
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerContext context;
        private readonly IMapper mapper;

        public TaskRepository(TaskManagerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TaskDTO> Create(TaskDTO task)
        {
            if(task == null)
            {
                return null;
            }

            var taskDb = mapper.Map<TaskDb>(task);
            await context.AddAsync(taskDb);
            await Save();

            return mapper.Map<TaskDTO>(taskDb);
        }

        public async Task<int> Delete(int id)
        {
            var taskDb = await context.Tasks.FindAsync(id);
            context.Tasks.Remove(taskDb);
            return await Save();
        }

        public async Task<IEnumerable<TaskDTO>> GetAll() => mapper.Map<IEnumerable<TaskDTO>>(await context.Tasks.ToListAsync());

        public async Task<TaskDTO> GetById(int id) => mapper.Map<TaskDTO>(await context.Tasks.FindAsync(id));

        public async Task<int> Save()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(TaskDTO task)
        {            
            var taskDb = await context.Tasks.FindAsync(task.Id);

            if (taskDb != null)
            {
                context.Entry(taskDb).CurrentValues.SetValues(mapper.Map<TaskDb>(task));
            }

            return await Save();
        }
    }
}
