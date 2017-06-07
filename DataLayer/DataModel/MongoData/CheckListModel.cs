using DomainModel.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel.MongoData
{
    class CheckListModel
    {
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("CheckItems")]
        public IEnumerable<ObjectId> CheckItems { get; set; }
    }
}
