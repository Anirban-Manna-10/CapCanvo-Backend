// CapCanvo.Core/Entities/BoardMember.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CapCanvo.Core.Common;
using CapCanvo.Core.Enums;

namespace CapCanvo.Core.Entities
{
    public class BoardMember : IdModel
    {
        [BsonElement("boardId")]
        public string BoardId { get; set; } = string.Empty;

        [BsonElement("userId")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("userName")]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("role")]
        [BsonRepresentation(BsonType.String)]
        public BoardRole Role { get; set; }

        [BsonElement("joinedAt")]
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}