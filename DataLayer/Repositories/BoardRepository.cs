using DataLayer.DataModel.MongoData;
using DomainModel.Entities;
using DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly MongoDBAccess db_;


        public BoardRepository(MongoDBAccess db)
        {
            db_ = db;
        }

        public BoardDTO CreateBoard(BoardDTO board)
        {
          var result =   db_.CreateBoard(board);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Someting vent rong");
            }
        }

        public IEnumerable<BoardDTO> GetAll()
        {
            return db_.GetAllBoards()
                 .Select(b => new BoardDTO(b.Id.ToString(), b.BoardName, b.Starred, b.Color));
        }

        public BoardDTO GetById(string id)
        {
            BoardModel board = db_.GetBoardById(id);
            IEnumerable<ListDTO> lists = db_.GetListByBoardId(id)
               .Select(l => new ListDTO(l.Id.ToString(), l.Name, l.Order, l.Description));

            return new BoardDTO(board.Id.ToString(), board.BoardName, board.Starred, lists);
        }

        public void update(BoardDTO model)
        {
            db_.updateBoard(model);
        }
    }
}
