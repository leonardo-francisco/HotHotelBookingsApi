using HHB.Domain.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.DTO
{
    public class HotelDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
        public int FoundedYear { get; set; }
        public int? ClosedYear { get; set; }
        public string Description { get; set; }
        public List<RoomDto>? Rooms { get; set; } = new();
    }
}
