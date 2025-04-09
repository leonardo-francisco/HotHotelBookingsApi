using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("foundedYear")]
        public int FoundedYear { get; set; }

        [BsonElement("closedYear")]
        [BsonIgnoreIfNull]
        public int? ClosedYear { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("rooms")]
        public List<Room>? Rooms { get; set; } = new();
    }
}
