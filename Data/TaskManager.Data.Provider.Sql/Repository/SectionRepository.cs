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
    public class SectionRepository : ISectionRepository
    {
        private readonly TaskManagerContext context;
        private readonly IMapper mapper;

        public SectionRepository(TaskManagerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<SectionDTO> Create(SectionDTO section)
        {
            if(section == null)
            {
                return null;
            }

            var sectionDb = mapper.Map<SectionDb>(section);
            await context.AddAsync(sectionDb);
            await Save();

            return mapper.Map<SectionDTO>(sectionDb);
        }

        public async Task<int> Delete(int id)
        {
            var sectionDb = await context.Sections.FindAsync(id);
            context.Sections.Remove(sectionDb);
            return await Save();
        }

        public async Task<IEnumerable<SectionDTO>> GetAll() => mapper.Map<IEnumerable<SectionDTO>>(await context.Sections.ToListAsync());

        public async Task<SectionDTO> GetById(int id) => mapper.Map<SectionDTO>(await context.Sections.FindAsync(id));

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

        public async Task<int> Update(SectionDTO section)
        {
            var sectionDb = await context.Sections.FindAsync(section.Id);

            if (sectionDb != null)
            {
                context.Entry(sectionDb).CurrentValues.SetValues(mapper.Map<SectionDb>(section));
            }
            return await Save();
        }
    }
}
