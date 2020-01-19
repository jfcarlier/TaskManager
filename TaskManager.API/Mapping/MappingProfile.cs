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
        //Route qu'AutoMapper va utiliser dans le projet
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
            CreateMap<BoardDTO, BoardDb>();
            CreateMap<SectionDTO, SectionDb>();
            CreateMap<TaskDTO, TaskDb>();

            //Mapping Data to DTO
            CreateMap<BoardDb, BoardDTO>();
            CreateMap<SectionDb, SectionDTO>();
            CreateMap<TaskDb, TaskDTO>();
        }
    }
}
