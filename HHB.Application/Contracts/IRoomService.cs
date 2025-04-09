using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Contracts
{
    public interface IRoomService : IService<RoomDto>
    {
        Task<IEnumerable<RoomDto>> GetByHotelIdAsync(string hotelId);
        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(string roomId);
    }
}
