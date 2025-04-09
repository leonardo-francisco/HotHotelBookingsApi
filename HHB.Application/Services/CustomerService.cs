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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> GetByIdAsync(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> InsertAsync(CustomerDto entity)
        {
            var customer = _mapper.Map<Customer>(entity);
            var result =  await _customerRepository.InsertAsync(customer);
            return _mapper.Map<CustomerDto>(result);
        }

        public async Task<CustomerDto> UpdateAsync(string id, CustomerDto entity)
        {
            var customer = _mapper.Map<Customer>(entity);
            var result =  await _customerRepository.UpdateAsync(id, customer);
            return _mapper.Map<CustomerDto>(result);
        }
    }
}
