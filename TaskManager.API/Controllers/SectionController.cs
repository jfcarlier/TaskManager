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
        public ActionResult<List<SectionAPI>> GetSections() => mapper.Map<IEnumerable<SectionAPI>>(domain.GetAllSections()).ToList();

        // GET: api/Section/5
        [HttpGet("{id}", Name = "GetS")]
        public string GetS(int id)
        {
            return "value";
        }

        // POST: api/Section
        [HttpPost]
        public void PostS([FromBody] string value)
        {
        }

        // PUT: api/Section/5
        [HttpPut("{id}")]
        public void PutS(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteS(int id)
        {
        }
    }
}
