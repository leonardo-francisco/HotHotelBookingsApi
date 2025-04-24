using HHB.Application.DTO;
using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Contracts
{
    public interface IBookingService : IService<BookingDto>
    {
        Task<IEnumerable<BookingDto>> GetByClientIdAsync(string clientId);
        Task<IEnumerable<BookingDto>> GetByHotelIdAsync(string hotelId);
        Task<IEnumerable<BookingDto>> GetByRoomIdAsync(string roomId);
        Task<IEnumerable<BookingDto>> GetActiveBookingsAsync();
        Task<BookingDto> SearchByNameAsync(string roomId, string name);
        Task<bool> IsAvailableAsync(string hotelId, string roomId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(string hotelId, DateTime checkIn, DateTime checkOut);
        Task<string> TryCheckInAsync(string id);
        Task<string> TryCheckOutAsync(string id);
        Task<string> TryCancelAsync(string id);
        Task<string> TryRefundAsync(string id);
        Task<string> AddServiceToBooking(BookingDto dto);
    }
}
