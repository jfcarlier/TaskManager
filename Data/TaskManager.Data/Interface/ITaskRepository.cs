using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;

namespace TaskManager.Data.Interface
{
    public interface ITaskRepository
    {
        IEnumerable<TaskDTO> GetAll();

        TaskDTO GetById(int id);

        TaskDTO Create(TaskDTO task);

        int Delete(int id);

        void Update(TaskDTO task);
    }
}
