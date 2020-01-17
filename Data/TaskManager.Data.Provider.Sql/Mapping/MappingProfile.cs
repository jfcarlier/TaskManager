using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.Core.Models;
using TaskManager.Data.Provider.Sql.Models;

namespace TaskManager.Data.Provider.Sql.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapping DTO to Data
            CreateMap<BoardDTO, Board>();
            CreateMap<SectionDTO, Section>();
            CreateMap<TaskDTO, Task>();

            //Mapping Data to DTO
            CreateMap<Board, BoardDTO>();
            CreateMap<Section, SectionDTO>();
            CreateMap<Task, TaskDTO>();
        }
    }
}
