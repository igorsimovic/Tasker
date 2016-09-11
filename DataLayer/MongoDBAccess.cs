﻿using DataLayer.DataModel.MongoData;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DataLayer
{
    public class MongoDBAccess
    {
        private MongoClient mongoClient_;
        private static MongoDBAccess instance_;
        private IMongoDatabase db_;

        public MongoDBAccess()
        {
            this.mongoClient_ = new MongoClient("mongodb://localhost:27017");
            this.db_ = mongoClient_.GetDatabase("TaskerDB");
        }

        public BoardDTO CreateBoard(BoardDTO board)
        {
            try
            {
                var boardModel = new BoardModel
                {
                    Color  = board.Color,
                    BoardName = board.BoardName
                };
                db_.GetCollection<BoardModel>("Boards").InsertOne(boardModel);
                return new BoardDTO
                {
                    Id = boardModel.Id.ToString(),
                    BoardName = boardModel.BoardName,
                    Color = boardModel.Color
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<BoardModel> GetAllBoards()
        {
            var filter = new BsonDocument();
            var result = db_.GetCollection<BoardModel>("Boards").Find(filter).ToEnumerable<BoardModel>();

            return result;
        }

        public BoardModel GetBoardById(string id)
        {
            var filter = Builders<BoardModel>.Filter.Eq("_id", new ObjectId(id));
            var result = db_.GetCollection<BoardModel>("Boards").Find(filter).FirstOrDefault();

            return result;
        }

        public void updateBoard(BoardDTO model)
        {
            var filter = Builders<BoardModel>.Filter.Eq("_id", new ObjectId(model.Id));
            BoardModel dbModel = new BoardModel
            {
                BoardName = model.BoardName,
                Color = model.Color,
                Starred = model.Starred,
                Id = new ObjectId(model.Id),
            };
            try
            {
                var result = db_.GetCollection<BoardModel>("Boards").ReplaceOne(filter, dbModel, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public IEnumerable<ListModel> GetListByBoardId(string boardId)
        {
            var board = this.GetBoardById(boardId);

            var filter = Builders<ListModel>.Filter.In("_id", board.Lists);
            var result = db_.GetCollection<ListModel>("Lists").Find(filter).ToEnumerable<ListModel>();

            return result;
        }

    }
}
