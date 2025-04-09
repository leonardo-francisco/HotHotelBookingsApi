using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.DTO
{
    public class BookingDto
    {
        public string? Id { get; set; }
        public int ClientId { get; set; }     
        public string HotelId { get; set; }
        public string RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public AdditionalServiceDto? AdditionalService { get; set; }
    }
}
