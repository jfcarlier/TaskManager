using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Data.Provider.Sql.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly TaskManagerContext context;
        private readonly IMapper mapper;
        public BoardRepository(TaskManagerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public BoardDTO Create(BoardDTO board)
        {
            if (board == null)
            {
                return null;
            }

            var boardDb = mapper.Map<BoardDb>(board);
            context.Add(boardDb);
            Save();

            return mapper.Map<BoardDTO>(boardDb);
        }

        public int Delete(int id)
        {
            var boardDb = context.Boards.Find(id);
            context.Boards.Remove(boardDb);
            return Save();
        }

        public IEnumerable<BoardDTO> GetAll() => mapper.Map<IEnumerable<BoardDTO>>(context.Boards);

        public BoardDTO GetById(int id) => mapper.Map<BoardDTO>(context.Boards.Find(id));

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(BoardDTO board)
        {
            var boardDb = context.Boards.Find(board.Id);

            if (boardDb != null)
            {
                context.Entry(boardDb).CurrentValues.SetValues(mapper.Map<BoardDb>(board));
            }
            return Save();
        }
    }
}
