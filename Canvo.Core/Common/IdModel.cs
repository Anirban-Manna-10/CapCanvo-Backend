using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CapCanvo.Core.Common;
public class IdModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; } = String.Empty;
}

