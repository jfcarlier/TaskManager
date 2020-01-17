using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;
using TaskManager.Core.Models;

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
        }
    }
}
