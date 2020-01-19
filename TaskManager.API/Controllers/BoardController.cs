using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.Business;
using TaskManager.Business.Interface;
using TaskManager.Core.Models;
using TaskManager.Data.Provider.Sql;
using TaskManager.Data.Provider.Sql.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ITaskManagerDomain domain;
        private readonly IMapper mapper;
        public BoardController(ITaskManagerDomain domain, IMapper mapper)
        {
            this.domain = domain;
            this.mapper = mapper;
        }

        // GET: api/Board
        [HttpGet]
        
        public ActionResult<IEnumerable<BoardAPI>> GetBoards() => mapper.Map<IEnumerable<BoardAPI>>(domain.GetAllBoards()).ToList();
        

        // GET: api/Board/5
        [HttpGet("{id}", Name = "GetBoardById")]
        public ActionResult<BoardAPI> GetBoardById(int id)
        {
            var board = mapper.Map<BoardAPI>(domain.GetBoardById(id));

            if (board == null)
            {
                return NotFound();
            }

            return board;
        }

        // POST: api/Board
        [HttpPost]
        public ActionResult<BoardAPI> CreateBoard(BoardAPI board)
        {            
            var boardTmp = domain.CreateBoard(mapper.Map<BoardDTO>(board));
            return mapper.Map<BoardAPI>(boardTmp);
        }

        // PUT: api/Board/5
        [HttpPut("{id}")]
        public ActionResult UpdateBoard(int id, BoardAPI board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            domain.UpdateBoard(mapper.Map<BoardDTO>(board));

            return Ok();
        }
    }
}
