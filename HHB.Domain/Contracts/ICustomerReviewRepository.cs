using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Contracts
{
    public interface ICustomerReviewRepository : IRepository<CustomerReview>
    {
        Task<IEnumerable<CustomerReview>> GetReviewHotelAsync(string hotelId);
        Task<IEnumerable<CustomerReview>> GetReviewRoomAsync(string roomId);
    }
}
