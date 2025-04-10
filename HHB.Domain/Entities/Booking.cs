using HHB.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Entities
{
    public class Booking : BaseEntity
    {
        [BsonElement("clientId")]
        public string ClientId { get; set; }
        [BsonElement("hotelId")]
        public string HotelId { get; set; }
        [BsonElement("roomId")]
        public string RoomId { get; set; }
        [BsonElement("checkIn")]
        public DateTime CheckIn { get; set; }
        [BsonElement("checkOut")]
        public DateTime CheckOut { get; set; }
      
        [BsonElement("status")]
        public BookingStatus Status { get; set; }
        [BsonElement("paymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }
        [BsonElement("additionalService")]
        public List<AdditionalService>? AdditionalService { get; set; }
    }
}
