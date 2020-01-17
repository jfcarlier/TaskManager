using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;

namespace TaskManager.Data.Interface
{
    public interface ISectionRepository
    {
        IEnumerable<SectionDTO> GetAll();

        SectionDTO GetById(int id);

        SectionDTO Create(SectionDTO section);

        int Delete(int id);

        void Update(SectionDTO section);

        int Save();
    }
}
