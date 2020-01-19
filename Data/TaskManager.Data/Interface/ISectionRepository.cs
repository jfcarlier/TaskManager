using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Data.Interface
{
    //Interface SectionRepository
    public interface ISectionRepository
    {
        Task<IEnumerable<SectionDTO>> GetAll();

        Task<SectionDTO> GetById(int id);

        Task<SectionDTO> Create(SectionDTO section);

        Task<int> Delete(int id);

        Task<int> Update(SectionDTO section);

        Task<int> Save();
    }
}
