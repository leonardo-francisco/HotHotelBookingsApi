using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Contracts
{
    public interface ICustomerService : IService<CustomerDto>
    {
        Task<CustomerDto> GetByEmailAsync(string email);
    }
}
