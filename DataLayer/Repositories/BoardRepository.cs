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

        public IEnumerable<BoardDTO> GetAll()
        {
           return db_.GetAllBoards()
                .Select(b=> new BoardDTO(b.Id.ToString(),b.BoardName,b.BoardStuff));
        }

        public BoardDTO GetById(string id)
        {
            BoardModel board = db_.GetBoardById(id);
            return new BoardDTO(board.Id.ToString(), board.BoardName, board.BoardStuff);
        }
    }
}
