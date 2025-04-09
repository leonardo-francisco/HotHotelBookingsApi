using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Contracts
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<IEnumerable<Hotel>> GetActiveHotelsAsync();
        Task<Hotel> GetByNameAsync(string name);
        Task<Hotel> GetByEmailAsync(string email);
    }
}
