using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Contracts
{
    public interface IBookingService : IService<BookingDto>
    {
        Task<IEnumerable<BookingDto>> GetByClientIdAsync(int clientId);
        Task<IEnumerable<BookingDto>> GetByHotelIdAsync(string hotelId);
        Task<IEnumerable<BookingDto>> GetByRoomIdAsync(string roomId);
        Task<IEnumerable<BookingDto>> GetActiveBookingsAsync();
    }
}
