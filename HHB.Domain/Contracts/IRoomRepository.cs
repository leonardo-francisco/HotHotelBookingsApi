using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Contracts
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetByHotelIdAsync(string hotelId);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(string hotelId);
    }
}
