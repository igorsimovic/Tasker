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

        public ListModel GetListById(string id)
        {
            var filter = Builders<ListModel>.Filter.Eq("_id", new ObjectId(id));
            var result = db_.GetCollection<ListModel>("Lists").Find(filter).FirstOrDefault();

            return result;
        }


        public IEnumerable<ListModel> GetListByBoardId(string boardId)
        {
            var board = this.GetBoardById(boardId);
            if (board.Lists != null)
            {
                var filter = Builders<ListModel>.Filter.In("_id", board.Lists);
                var result = db_.GetCollection<ListModel>("Lists").Find(filter).ToEnumerable<ListModel>();
                return result;
            }
            return null;
        }
        public IEnumerable<CardModel> GetCardsByListId(string id)
        {
            var list = this.GetListById(id);

            if (list.Cards != null)
            {
                var filter = Builders<CardModel>.Filter.In("_id", list.Cards);
                var result = db_.GetCollection<CardModel>("Cards").Find(filter).ToEnumerable<CardModel>();
                return result;
            }
            return null;
        }

    }
}
