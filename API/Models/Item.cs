using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace gameapi.Models{
    public class Item    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
    }
    public class NewItem    {
        public int Level { get; set; }
        public string Type { get; set; }
    }
    public class ModifiedItem    {
        public int Price { get; set; }
    }
}