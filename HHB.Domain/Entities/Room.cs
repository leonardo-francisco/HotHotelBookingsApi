using HHB.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Entities
{
    public class Room : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("hotelId")]
        public string HotelId { get; set; }
        [BsonElement("number")]
        public string Number { get; set; }
        [BsonElement("roomType")]
        public RoomType RoomType { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("capacity")]
        public int Capacity { get; set; }
        [BsonElement("pricePerNight")]
        public decimal PricePerNight { get; set; }
        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }
    }
}
