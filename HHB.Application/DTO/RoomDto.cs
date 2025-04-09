using HHB.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.DTO
{
    public class RoomDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string HotelId { get; set; }
        public string Number { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
