using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlocks.BaseEntities
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime? DateModified { get; protected set; }

        protected BaseEntity()
        {
            Id =ObjectId.GenerateNewId().ToString();
            DateCreated = DateTime.UtcNow;
        }

        public void SetDateCreated()
        {
            DateCreated =DateTime.UtcNow;
        }
        public void UpdateModificationDate()
        {
            DateModified = DateTime.UtcNow;
        }
    }
}

