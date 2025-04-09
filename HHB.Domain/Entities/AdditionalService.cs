using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Entities
{
    public class AdditionalService : BaseEntity
    {
        [BsonElement("hotelId")]
        public string HotelId { get; set; }
        [BsonElement("serviceName")]
        public string ServiceName { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }
    }
}
