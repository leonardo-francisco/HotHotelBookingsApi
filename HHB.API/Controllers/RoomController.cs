using FluentValidation;
using HHB.Application.Contracts;
using HHB.Application.DTO;
using HHB.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HHB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;
        private readonly IValidator<RoomDto> _validator;

        public RoomController(IRoomService roomService, IHotelService hotelService, IValidator<RoomDto> validator)
        {
            _roomService = roomService;
            _hotelService = hotelService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _roomService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(string id)
        {
            var result = await _roomService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("allRooms/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotelId(string hotelId)
        {
            var result = await _roomService.GetByHotelIdAsync(hotelId);

            return Ok(result);
        }

        [HttpGet("availableRooms/{id}")]
        public async Task<IActionResult> GetAllAvailableRooms(string hotelId)
        {
            var result = await _roomService.GetAvailableRoomsAsync(hotelId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDto roomDto)
        {
            var validationResult = await _validator.ValidateAsync(roomDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var hotel = await _hotelService.GetByIdAsync(roomDto.HotelId);
            if (hotel == null)
                return NotFound($"Hotel com ID {roomDto.HotelId} não foi encontrado.");

            var insertedRoom = await _roomService.InsertAsync(roomDto);

            hotel.Rooms ??= new List<RoomDto>();
            hotel.Rooms.Add(insertedRoom);
            
            await _hotelService.UpdateAsync(hotel.Id, hotel);

            return Ok(new
            {
                message = "Quarto cadastrado com sucesso",
                data = insertedRoom
            });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(string id, [FromBody] RoomDto roomDto)
        {
            var validationResult = await _validator.ValidateAsync(roomDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var existingRoom = await _roomService.GetByIdAsync(id);
            if (existingRoom == null)
                return NotFound($"Quarto com ID {id} não foi encontrado.");
            
            roomDto.Id = existingRoom.Id;
            var updatedRoom = await _roomService.UpdateAsync(id, roomDto);

            var hotel = await _hotelService.GetByIdAsync(roomDto.HotelId);
            if (hotel == null)
                return NotFound($"Hotel com ID {roomDto.HotelId} não foi encontrado.");

            var rooms = await _roomService.GetByHotelIdAsync(hotel.Id);

            hotel.Rooms = (List<RoomDto>?)rooms;

            if (hotel.Rooms != null)
            {
                var roomIndex = hotel.Rooms.FindIndex(r => r.Id == id);
                if (roomIndex != -1)
                {
                    hotel.Rooms[roomIndex] = updatedRoom;
                }
                else
                {
                    hotel.Rooms.Add(updatedRoom);
                }
            }
            else
            {
                hotel.Rooms = new List<RoomDto> { updatedRoom };
            }

            await _hotelService.UpdateAsync(hotel.Id, hotel);

            return Ok(new
            {
                message = "Quarto atualizado com sucesso",
                data = updatedRoom
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRoom(string id)
        {           
            var room = await _roomService.GetByIdAsync(id);
            if (room == null)
                return NotFound($"Quarto com ID {id} não foi encontrado.");
          
            var hotel = await _hotelService.GetByIdAsync(room.HotelId);
            if (hotel != null && hotel.Rooms != null)
            {
                hotel.Rooms.RemoveAll(r => r.Id == id);
                await _hotelService.UpdateAsync(hotel.Id, hotel);
            }
          
            await _roomService.DeleteAsync(id);

            return NoContent();
        }
    }
}
