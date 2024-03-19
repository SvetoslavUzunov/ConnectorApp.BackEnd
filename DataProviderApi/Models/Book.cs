using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataProviderApi.Models;

[BsonIgnoreExtraElements]
public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("isbn")]
    public string Isbn { get; set; } = string.Empty;

    [BsonElement("pageCount")]
    public int PageCount { get; set; }

    [BsonElement("authors")]
    public List<string> Authors { get; set; } = [];
}
