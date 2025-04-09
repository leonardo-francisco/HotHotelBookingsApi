using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Contracts
{
    public interface ICustomerReviewService : IService<CustomerReviewDto>
    {
        Task<IEnumerable<CustomerReviewDto>> GetReviewHotelAsync(string hotelId);
        Task<IEnumerable<CustomerReviewDto>> GetReviewRoomAsync(string roomId);
    }
}
