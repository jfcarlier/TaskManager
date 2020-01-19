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
        
        public async Task<ActionResult<IEnumerable<BoardAPI>>> GetBoards() => mapper.Map<IEnumerable<BoardAPI>>(await domain.GetAllBoards()).ToList();
        

        // GET: api/Board/5
        [HttpGet("{id}", Name = "GetBoardById")]
        public async Task<ActionResult<BoardAPI>> GetBoardById(int id)
        {
            var board = mapper.Map<BoardAPI>(await domain.GetBoardById(id));

            if (board == null)
            {
                return NotFound();
            }

            return board;
        }

        // POST: api/Board
        [HttpPost]
        public async Task<ActionResult<BoardAPI>> CreateBoard(BoardAPI board)
        {
            if(board == null)
            {
                return BadRequest();
            }
            var boardTmp = await domain.CreateBoard(mapper.Map<BoardDTO>(board));
            return mapper.Map<BoardAPI>(boardTmp);
        }

        // PUT: api/Board/5
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateBoard(int id, BoardAPI board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            var response = await domain.UpdateBoard(mapper.Map<BoardDTO>(board));

            if(response > 0)
            {
                return $"{response} Board updated";
            }
            return BadRequest();
        }
    }
}
