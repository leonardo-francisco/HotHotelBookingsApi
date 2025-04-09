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
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string id)
        {
            await _hotelRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<HotelDto>> GetActiveHotelsAsync()
        {
            var hotels = await _hotelRepository.GetActiveHotelsAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<IEnumerable<HotelDto>> GetAllAsync()
        {
            var hotels = await _hotelRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<HotelDto> GetByEmailAsync(string email)
        {
            var hotel = await _hotelRepository.GetByEmailAsync(email);
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> GetByIdAsync(string id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> GetByNameAsync(string name)
        {
            var hotel = await _hotelRepository.GetByNameAsync(name);
            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> InsertAsync(HotelDto entity)
        {
            var hotel = _mapper.Map<Hotel>(entity);
            var result = await _hotelRepository.InsertAsync(hotel);
            return _mapper.Map<HotelDto>(result);
        }

        public async Task<HotelDto> UpdateAsync(string id, HotelDto entity)
        {
            var hotel = _mapper.Map<Hotel>(entity);
            var result = await _hotelRepository.UpdateAsync(id, hotel);
            return _mapper.Map<HotelDto>(result);
        }
    }
}
