using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel.MongoData
{
    public class CommentModel
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }

        [BsonElement("Text")]
        public string Text { get; set; }
       
    }
}
