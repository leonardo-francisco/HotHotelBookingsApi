using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Contracts
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetByClientIdAsync(string clientId);
        Task<IEnumerable<Booking>> GetByHotelIdAsync(string hotelId);
        Task<IEnumerable<Booking>> GetByRoomIdAsync(string roomId);
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();
        Task<IEnumerable<Booking>> GetByHotelAndRoomAsync(string hotelId, string roomId);
        Task<Booking> SearchByName(string roomId, string name);
    }
}
