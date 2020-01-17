using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;

namespace TaskManager.Data.Provider.Sql.Repository
{
    public class TaskRepository : ITaskRepository
    {
        public TaskDTO Create(TaskDTO task)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaskDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TaskDTO task)
        {
            throw new NotImplementedException();
        }
    }
}
