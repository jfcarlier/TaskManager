﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<TaskAPI>>> GetTasks() => mapper.Map<IEnumerable<TaskAPI>>(await domain.GetAllTasks()).ToList();

        // GET: api/Task/5
        [HttpGet("{id}", Name = "GetTaskById")]
        public async Task<ActionResult<TaskAPI>> GetTaskById(int id)
        {
            var task = mapper.Map<TaskAPI>(await domain.GetTaskById(id));

            if(task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/Task/Board/2
        [HttpPost("board/{id}")]
        //id correspond à l'id du tableau dans lequel se trouve la tâche
        public async Task<ActionResult<TaskAPI>> CreateTask(int id,TaskAPI task)
        {
            var taskTmp = await domain.CreateTask(id, mapper.Map<TaskDTO>(task));

            if(taskTmp == null)
            {
                return BadRequest();
            }

            return mapper.Map<TaskAPI>(taskTmp);
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateTask(int id, TaskAPI task)
        {
            if(id != task.Id)
            {
                return BadRequest();
            }

            var response = await domain.UpdateTask(mapper.Map<TaskDTO>(task));
            
            if(response > 0)
            {
                return $"{response} Task updated";
            }

            return BadRequest();
        }

        //PUT: api/Task/changesection/2
        [HttpPut("ChangeSection/{id}")]
        public async Task<ActionResult<string>> ChangeSectionTask(int id, TaskAPI task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var response = await domain.ChanseSectionTask(id, mapper.Map<TaskDTO>(task));

            if (response > 0)
            {
                return $"{response} SectionTask updated";
            }

            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteTask(int id)
        {
            var response = await domain.DeleteTask(id);

            if(response > 0)
            {
                return $"{response} Task deleted";
            }
            return BadRequest();            
        }
    }
}
