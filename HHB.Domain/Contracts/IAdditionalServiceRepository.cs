using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Contracts
{
    public interface IAdditionalServiceRepository : IRepository<AdditionalService>
    {
        Task<IEnumerable<AdditionalService>> GetByHotelIdAsync(string hotelId);
        Task<IEnumerable<AdditionalService>> GetAvailableServicesAsync(string hotelId);
    }
}
