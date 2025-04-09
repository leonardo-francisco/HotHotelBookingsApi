using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Entities
{
    public class CustomerReview : BaseEntity
    {
        [BsonElement("hotelId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HotelId { get; set; } // Reference to the reviewed hotel
        [BsonElement("roomId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RoomId { get; set; }

        [BsonElement("customerName")]
        public string CustomerName { get; set; }

        [BsonElement("reviewDate")]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        [BsonElement("rating")]
        public int Rating { get; set; } // Score from 1 to 5

        [BsonElement("title")]
        public string Title { get; set; } // Short summary

        [BsonElement("comment")]
        public string Comment { get; set; } // Detailed feedback

        [BsonElement("serviceRating")]
        public int ServiceRating { get; set; } // Optional: rating specifically for service

        [BsonElement("cleanlinessRating")]
        public int CleanlinessRating { get; set; } // Optional: rating for cleanliness

        [BsonElement("wouldRecommend")]
        public bool WouldRecommend { get; set; }
    }
}
