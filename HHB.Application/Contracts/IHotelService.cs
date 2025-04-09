using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Contracts
{
    public interface IHotelService : IService<HotelDto>
    {
        Task<IEnumerable<HotelDto>> GetActiveHotelsAsync();
        Task<HotelDto> GetByNameAsync(string name);
        Task<HotelDto> GetByEmailAsync(string email);
    }
}
