using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Data.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDTO>> GetAll();

        Task<TaskDTO> GetById(int id);

        Task<TaskDTO> Create(TaskDTO task);

        Task<int> Delete(int id);

        Task<int> Update(TaskDTO task);

        Task<int> Save();
    }
}
