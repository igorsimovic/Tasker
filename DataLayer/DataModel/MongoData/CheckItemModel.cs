using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel.MongoData
{
    public class CheckItemModel
    {

        public ObjectId Id { get; set; }
        [BsonElement("Value")]
        public string Value { get; set; }
        [BsonElement("Checked")]
        public bool Checked { get; set; }
    }
}
