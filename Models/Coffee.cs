using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace l12_nosql.Models;

public class Coffee 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [Required]
    [BsonElement("Name")]
    public string? Name { get; set; }
    [Required]
    [BsonElement("Description")]
    public string? Description { get; set; }
    [Required]
    [BsonElement("Price")]
    public double Price { get; set; }
}