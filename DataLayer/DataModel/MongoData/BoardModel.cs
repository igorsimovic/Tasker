using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel.MongoData
{
   public class BoardModel
    {
        public ObjectId Id { get; set; }
        [BsonElement("BoardName")]
        public string BoardName { get; set; }
        [BsonElement("BoardStuff")]
        public string BoardStuff { get; set; }

        [BsonElement("Lists")]
        public IEnumerable<ObjectId> Lists{ get; set; }
        [BsonElement("Starred")]
        public bool Starred { get; set; }
    }
}
