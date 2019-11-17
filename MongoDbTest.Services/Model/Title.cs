using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbTest.Services.Model
{
    public class Title : MongoBaseModel
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
