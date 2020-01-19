using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.Business.Interface;
using TaskManager.Core.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManagerDomain domain;
        private readonly IMapper mapper;

        public TaskController(ITaskManagerDomain domain, IMapper mapper)
        {
            this.domain = domain;
            this.mapper = mapper;
        }

        // GET: api/Task
        [HttpGet]
        public ActionResult<IEnumerable<TaskAPI>> GetTasks() => mapper.Map<IEnumerable<TaskAPI>>(domain.GetAllTasks()).ToList();

        // GET: api/Task/5
        [HttpGet("{id}", Name = "GetTaskById")]
        public ActionResult<TaskAPI> GetTaskById(int id)
        {
            var task = mapper.Map<TaskAPI>(domain.GetTaskById(id));

            if(task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/Task
        [HttpPost("board/{id}")]
        public ActionResult<TaskAPI> CreateTask(int id,TaskAPI task)
        {
            var taskTmp = domain.CreateTask(id, mapper.Map<TaskDTO>(task));

            if(taskTmp == null)
            {
                return BadRequest();
            }

            return mapper.Map<TaskAPI>(taskTmp);
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public ActionResult<string> UpdateTask(int id, TaskAPI task)
        {
            if(id != task.Id)
            {
                return BadRequest();
            }

            var response = domain.UpdateTask(mapper.Map<TaskDTO>(task));
            
            if(response > 0)
            {
                return $"{response} Task updated";
            }

            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteTask(int id)
        {
            var response = domain.DeleteTask(id);

            if(response > 0)
            {
                return $"{response} Task deleted";
            }
            return BadRequest();            
        }
    }
}
