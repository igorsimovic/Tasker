﻿using DataLayer.DataModel.MongoData;
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
            var result = db_.CreateBoard(board);
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
                 .Select(b => new BoardDTO(b.Id.ToString(), b.BoardName, b.Starred, b.Color, b.OrderNo));
        }

        public List<BoardDTO> GetBoardsByUserID(string userID)
        {
            var boardsModel = db_.getBoardsByUser(userID).ToList();
            return boardsModel.Select(b => new BoardDTO(b.Id.ToString(), b.BoardName, b.Starred, b.Color, b.OrderNo)).ToList();
        }

        public BoardDTO GetById(string id)
        {
            BoardModel board = db_.GetBoardById(id);
            IEnumerable<ListDTO> lists = new List<ListDTO>();

            var tempLists = db_.GetListByBoardId(id);
            if (tempLists != null)
            {
                lists = tempLists.Select(l => new ListDTO(l.Id.ToString(), l.Name, l.Order, l.Description)).OrderBy(l => l.Order).ToList();
                foreach (var list in lists)
                {
                    var tempCards = db_.GetCardsByListId(list.Id);
                    if (tempCards != null)
                    {
                        list.Cards = tempCards.Select(c => new CardDTO(c.Id.ToString(), c.Name, c.Order, c.Description)).OrderBy(c => c.Order);
                    }
                }
            }


            return new BoardDTO(board.Id.ToString(), board.BoardName, board.Starred, lists);
        }

        public void update(BoardDTO model)
        {
            db_.updateBoard(model);
        }
    }
}
