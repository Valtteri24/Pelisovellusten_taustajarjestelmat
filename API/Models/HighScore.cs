using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace gameapi.Models{
    public class HighScore    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public Item[] Items { get; set; }
    }
    public class NewHighScore    {
        [Required]
        public string Name { get; set; }
    }
    public class ModifiedHighScore    {
        [Required]
        public int Level { get; set; }
    }
}