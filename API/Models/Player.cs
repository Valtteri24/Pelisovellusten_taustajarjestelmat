using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace gameapi.Models{
    public class Player    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public Item[] Items { get; set; }
    }
    public class NewPlayer    {
        [Required]
        public string Name { get; set; }
    }
    public class ModifiedPlayer    {
        [Required]
        public int Level { get; set; }
    }
}