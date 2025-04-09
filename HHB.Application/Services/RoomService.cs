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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string id)
        {
            await _roomRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(string roomId)
        {
            var rooms = await _roomRepository.GetAvailableRoomsAsync(roomId);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<IEnumerable<RoomDto>> GetByHotelIdAsync(string hotelId)
        {
            var rooms = await _roomRepository.GetByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetByIdAsync(string id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> InsertAsync(RoomDto entity)
        {
            var room = _mapper.Map<Room>(entity);
            var result = await _roomRepository.InsertAsync(room);
            return _mapper.Map<RoomDto>(result);
        }

        public async Task<RoomDto> UpdateAsync(string id, RoomDto entity)
        {
            var room = _mapper.Map<Room>(entity);
            var result = await _roomRepository.UpdateAsync(id, room);
            return _mapper.Map<RoomDto>(result);
        }
    }
}
