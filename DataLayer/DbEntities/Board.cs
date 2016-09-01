using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbEntities
{
    public class Board
    {
        public ObjectId Id { get; set; }
        [BsonElement("BoardName")]
        public string BoardName { get; set; }
        [BsonElement("BoardStuff")]
        public string BoardStuff { get; set; }
    }
}
