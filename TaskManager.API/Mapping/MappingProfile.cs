using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.API.Models;
using TaskManager.Core.Models;
using TaskManager.Data.Provider.Sql.Models;

namespace TaskManager.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapping DTO to API
            CreateMap<BoardDTO, BoardAPI>();
            CreateMap<SectionDTO, SectionAPI>();
            CreateMap<TaskDTO, TaskAPI>();

            //Mapping API to DTO
            CreateMap<BoardAPI, BoardDTO>();
            CreateMap<SectionAPI, SectionDTO>();
            CreateMap<TaskAPI, TaskDTO>();

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
