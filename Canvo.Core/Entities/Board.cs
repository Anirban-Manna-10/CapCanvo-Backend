// CapCanvo.Core/Entities/Board.cs
using MongoDB.Bson.Serialization.Attributes;
using CapCanvo.Core.Common;

namespace CapCanvo.Core.Entities
{
    public class Board : IdModel
    {
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("ownerId")]
        public string OwnerId { get; set; } = string.Empty;

        [BsonElement("ownerName")]
        public string OwnerName { get; set; } = string.Empty;

        [BsonElement("shareToken")]
        public string ShareToken { get; set; } = Guid.NewGuid().ToString("N");

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public List<BoardMember> BoardMember { get; set; } = new();
    }
}