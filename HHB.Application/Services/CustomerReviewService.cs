using AutoMapper;
using HHB.Application.Contracts;
using HHB.Application.DTO;
using HHB.Domain.Contracts;
using HHB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Services
{
    public class CustomerReviewService : ICustomerReviewService
    {
        private readonly ICustomerReviewRepository _customerReviewRepository;
        private readonly IMapper _mapper;

        public CustomerReviewService(ICustomerReviewRepository customerReviewRepository, IMapper mapper)
        {
            _customerReviewRepository = customerReviewRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string id)
        {
            await _customerReviewRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CustomerReviewDto>> GetAllAsync()
        {
            var reviews = await _customerReviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerReviewDto>>(reviews);
        }

        public async Task<CustomerReviewDto> GetByIdAsync(string id)
        {
            var review = await _customerReviewRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerReviewDto>(review);
        }

        public async Task<IEnumerable<CustomerReviewDto>> GetReviewHotelAsync(string hotelId)
        {
            var reviews = await _customerReviewRepository.GetReviewHotelAsync(hotelId);
            return _mapper.Map<IEnumerable<CustomerReviewDto>>(reviews);
        }

        public async Task<IEnumerable<CustomerReviewDto>> GetReviewRoomAsync(string roomId)
        {
            var reviews = await _customerReviewRepository.GetReviewRoomAsync(roomId);
            return _mapper.Map<IEnumerable<CustomerReviewDto>>(reviews);
        }

        public async Task<CustomerReviewDto> InsertAsync(CustomerReviewDto entity)
        {
            var review = _mapper.Map<CustomerReview>(entity);
            var result = await _customerReviewRepository.InsertAsync(review);
            return _mapper.Map<CustomerReviewDto>(result);
        }

        public async Task<CustomerReviewDto> UpdateAsync(string id, CustomerReviewDto entity)
        {
            var review = _mapper.Map<CustomerReview>(entity);
            var result = await _customerReviewRepository.UpdateAsync(id, review);
            return _mapper.Map<CustomerReviewDto>(result);
        }
    }
}
