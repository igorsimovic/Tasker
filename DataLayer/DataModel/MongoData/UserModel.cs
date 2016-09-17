using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel.MongoData
{
    public class UserModel
    {

        public ObjectId Id { get; set; }
        [BsonElement("fullName")]
        public string FullName { get; set; }
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("initials")]
        public string Initials { get; set; }
        [BsonElement("bio")]
        public string Bio { get; set; }
        [BsonElement("picture")]
        public string Picture { get; set; }
        [BsonElement("newPassword")]
        public string NewPassword { get; set; }
        [BsonElement("oldPassword")]
        public string OldPassword { get; set; }
        [BsonElement("repeatPassword")]
        public string RepeatPassword { get; set; }
        [BsonElement("boards")]
        public List<ObjectId> Boards { get; set; }

    }
}
