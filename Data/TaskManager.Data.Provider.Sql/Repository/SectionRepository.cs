using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Models;

namespace TaskManager.Data.Provider.Sql.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly TaskManagerContext context;
        private readonly IMapper mapper;

        public SectionRepository(TaskManagerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public SectionDTO Create(SectionDTO section)
        {
            if(section == null)
            {
                return null;
            }

            var sectionDb = mapper.Map<Section>(section);
            context.Add(sectionDb);
            Save();

            return mapper.Map<SectionDTO>(sectionDb);
        }

        public int Delete(int id)
        {
            var sectionDb = context.Sections.Find(id);
            context.Sections.Remove(sectionDb);
            return Save();
        }

        public IEnumerable<SectionDTO> GetAll() => mapper.Map<IEnumerable<SectionDTO>>(context.Sections);

        public SectionDTO GetById(int id) => mapper.Map<SectionDTO>(context.Sections.Find(id));

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

        public void Update(SectionDTO section)
        {
            if(section == null)
            {
                return;
            }

            var sectionDb = context.Sections.Find(section.Id);
            context.Entry(sectionDb).CurrentValues.SetValues(mapper.Map<Section>(section));
            Save();
        }
    }
}
