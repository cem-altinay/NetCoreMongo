using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbTest.Services.Model
{
    public class Users : MongoBaseModel
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("IsDeleted")]
        public bool IsDeleted { get; set; }

        public Title Title { get; set; }
    }
}
