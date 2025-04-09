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
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string id)
        {
            await _bookingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookingDto>> GetActiveBookingsAsync()
        {
            var bookings = await _bookingRepository.GetActiveBookingsAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetByClientIdAsync(int clientId)
        {
            var bookings = await _bookingRepository.GetByClientIdAsync(clientId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetByHotelIdAsync(string hotelId)
        {
            var bookings = await _bookingRepository.GetByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetByIdAsync(string id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetByRoomIdAsync(string roomId)
        {
            var bookings = await _bookingRepository.GetByRoomIdAsync(roomId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> InsertAsync(BookingDto entity)
        {
            var booking = _mapper.Map<Booking>(entity);
            var result = await _bookingRepository.InsertAsync(booking);
            return _mapper.Map<BookingDto>(result);
        }

        public async Task<BookingDto> UpdateAsync(string id, BookingDto entity)
        {
            var booking = _mapper.Map<Booking>(entity);
            var result = await _bookingRepository.UpdateAsync(id, booking);
            return _mapper.Map<BookingDto>(result);
        }
    }
}
