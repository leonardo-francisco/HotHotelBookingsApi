using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.DTO
{
    public class CustomerReviewDto
    {
        public string? Id { get; set; }
        public string HotelId { get; set; } 
        public string RoomId { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
        public int Rating { get; set; } 
        public string Title { get; set; } 
        public string Comment { get; set; } 
        public int ServiceRating { get; set; } 
        public int CleanlinessRating { get; set; } 
        public bool WouldRecommend { get; set; }
    }
}
