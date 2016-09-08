using DataLayer.DataModel.MongoData;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<ListModel> GetListByBoardId(string boardId)
        {
            var board = this.GetBoardById(boardId);

            var filter = Builders<ListModel>.Filter.In("_id", board.Lists);
            var result = db_.GetCollection<ListModel>("Lists").Find(filter).ToEnumerable<ListModel>();

            return result;
        }

    }
}
