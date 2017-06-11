using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel.MongoData
{
    public class CardModel
    {
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Order")]
        public int Order { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Labels")]
        public IEnumerable<ObjectId> Labels { get; set; }

        [BsonElement("Comments")]
        public IEnumerable<CommentModel> Comments { get; set; }

        [BsonElement("CheckLists")]
        public IEnumerable<ObjectId> CheckLists { get; set; }

        [BsonElement("DueDate")]
        public string DueDate { get; set; }
    }
}
