using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Models;

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
            if(board == null)
            {
                return null;
            }

            var boardDb = mapper.Map<Board>(board);
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

        public void Update(BoardDTO board)
        {
            if(board == null)
            {
                return;
            }

            var boardDb = context.Boards.Find(board.Id);
            context.Entry(boardDb).CurrentValues.SetValues(mapper.Map<Board>(board));
            Save();
        }
    }
}
