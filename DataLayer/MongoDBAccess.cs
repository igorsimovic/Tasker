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
            return db_.GetCollection<BoardModel>("Boards").Find(filter).ToEnumerable<BoardModel>();
        }
    }
}
