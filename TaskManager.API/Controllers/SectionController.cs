using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.Business.Interface;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ITaskManagerDomain domain;
        private readonly IMapper mapper;

        public SectionController(ITaskManagerDomain domain, IMapper mapper)
        {
            this.domain = domain;
            this.mapper = mapper;
        }

        // GET: api/Section
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectionAPI>>> GetSections() => mapper.Map<IEnumerable<SectionAPI>>(await domain.GetAllSections()).ToList();

        // GET: api/Section/5
        [HttpGet("{id}", Name = "GetSectionById")]
        public async Task<ActionResult<SectionAPI>> GetSectionById(int id)
        {
            var section = mapper.Map<SectionAPI>(await domain.GetSectionById(id));

            if(section == null)
            {
                return NotFound();
            }

            return section;
        }        
    }
}
