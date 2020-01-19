using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaskManager.Data.Provider.Sql.Repository
{
    //CRUD
    public class BoardRepository : IBoardRepository
    {
        private readonly TaskManagerContext context;
        private readonly IMapper mapper;
        public BoardRepository(TaskManagerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<BoardDTO> Create(BoardDTO board)
        {
            if (board == null)
            {
                return null;
            }

            var boardDb = mapper.Map<BoardDb>(board);
            await context.AddAsync(boardDb);
            await Save();

            return mapper.Map<BoardDTO>(boardDb);
        }

        public async Task<int> Delete(int id)
        {
            var boardDb = await context.Boards.FindAsync(id);
            context.Boards.Remove(boardDb);
            return await Save();
        }

        public async Task<IEnumerable<BoardDTO>> GetAll() => mapper.Map<IEnumerable<BoardDTO>>(await context.Boards.ToListAsync());

        public async Task<BoardDTO> GetById(int id) => mapper.Map<BoardDTO>(await context.Boards.FindAsync(id));

        public async Task<int> Save()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(BoardDTO board)
        {
            var boardDb = await context.Boards.FindAsync(board.Id);

            if (boardDb != null)
            {
                context.Entry(boardDb).CurrentValues.SetValues(mapper.Map<BoardDb>(board));
            }
            return await Save();
        }
    }
}
