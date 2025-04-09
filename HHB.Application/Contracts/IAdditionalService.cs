using HHB.Application.DTO;
using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Contracts
{
    public interface IAdditionalService : IService<AdditionalServiceDto>
    {
        Task<IEnumerable<AdditionalServiceDto>> GetByHotelIdAsync(string hotelId);
        Task<IEnumerable<AdditionalServiceDto>> GetAvailableServicesAsync(string hotelId);
    }
}
