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
    public class AdditionalService : IAdditionalService
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;
        public AdditionalService(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper)
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string id)
        {
            await _additionalServiceRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AdditionalServiceDto>> GetAllAsync()
        {
            var additional = await _additionalServiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AdditionalServiceDto>>(additional);
        }

        public async Task<IEnumerable<AdditionalServiceDto>> GetAvailableServicesAsync(string hotelId)
        {
            var additional = await _additionalServiceRepository.GetAvailableServicesAsync(hotelId);
            return _mapper.Map<IEnumerable<AdditionalServiceDto>>(additional);
        }

        public async Task<IEnumerable<AdditionalServiceDto>> GetByHotelIdAsync(string hotelId)
        {
            var additional = await _additionalServiceRepository.GetByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<AdditionalServiceDto>>(additional);
        }

        public async Task<AdditionalServiceDto> GetByIdAsync(string id)
        {
            var additional = await _additionalServiceRepository.GetByIdAsync(id);
            return _mapper.Map<AdditionalServiceDto>(additional);
        }

        public async Task<AdditionalServiceDto> InsertAsync(AdditionalServiceDto entity)
        {
            var additional = _mapper.Map<Domain.Entities.AdditionalService>(entity);
            var result = await _additionalServiceRepository.InsertAsync(additional);
            return _mapper.Map<AdditionalServiceDto>(result);
        }

        public async Task<AdditionalServiceDto> UpdateAsync(string id, AdditionalServiceDto entity)
        {
            var additional = _mapper.Map<Domain.Entities.AdditionalService>(entity);
            var result = await _additionalServiceRepository.UpdateAsync(id, additional);
            return _mapper.Map<AdditionalServiceDto>(result);
        }
    }
}
