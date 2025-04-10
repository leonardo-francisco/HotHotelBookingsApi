using FluentValidation;
using HHB.Application.Contracts;
using HHB.Application.DTO;
using HHB.Application.Services;
using HHB.Domain.Entities;
using HHB.Domain.Enums;
using HHB.Domain.Rules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HHB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;  
        private readonly IValidator<BookingDto> _validator;

        public BookingController(IBookingService bookingService, IRoomService roomService, IValidator<BookingDto> validator)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(string id)
        {
            var result = await _bookingService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("availability")]
        public async Task<IActionResult> CheckAvailability(
                                         [FromQuery] string hotelId,
                                         [FromQuery] string roomId,
                                         [FromQuery] string startDate,
                                         [FromQuery] string endDate)
        {
            if (!DateTime.TryParseExact(startDate.ToString(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var start))
                return BadRequest("Data de início inválida. Use o formato yyyy-MM-dd");

            if (!DateTime.TryParseExact(endDate.ToString(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var end))
                return BadRequest("Data de fim inválida. Use o formato yyyy-MM-dd");

            var isAvailable = await _bookingService.IsAvailableAsync(hotelId, roomId, start, end);
            return Ok(new { Available = isAvailable });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
        {
            var validationResult = await _validator.ValidateAsync(bookingDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var insertedCustomer = await _bookingService.InsertAsync(bookingDto);

            return Ok(new
            {
                message = "Reserva criada com sucesso",
                data = insertedCustomer
            });

        }

        [HttpPost("{id}/checkin")]
        public async Task<IActionResult> CheckIn(string id)
        {
            var error = await _bookingService.TryCheckInAsync(id);

            if (error != null)
                return BadRequest(error);

            return Ok("Check-in realizado com sucesso.");
        }

        [HttpPost("{id}/checkout")]
        public async Task<IActionResult> CheckedOut(string id)
        {
            var error = await _bookingService.TryCheckOutAsync(id);

            if (error != null)
                return BadRequest(error);
           
            return Ok("Check-out realizado com sucesso.");
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(string id)
        {
            var error = await _bookingService.TryCancelAsync(id);

            if (error != null)
                return BadRequest(error);
            
            return Ok("Reserva cancelada com sucesso.");
        }

        [HttpPost("{id}/refund")]
        public async Task<IActionResult> Refund(string id)
        {
            var error = await _bookingService.TryRefundAsync(id);

            if (error != null)
                return BadRequest(error);
          
            return Ok("Reembolso processado com sucesso.");
        }

        [HttpPut("addServiceToBooking")]
        public async Task<IActionResult> AddServiceToBooking([FromBody] BookingDto bookingDto)
        {
           var error = await _bookingService.AddServiceToBooking(bookingDto);

            if (error != null)
                return BadRequest(error);


            return Ok("Serviços adicionados à reserva com sucesso.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(string id, [FromBody] BookingDto bookingDto)
        {
            var validationResult = await _validator.ValidateAsync(bookingDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
           
            var updatedCustomer = await _bookingService.UpdateAsync(id, bookingDto);

            return Ok(new
            {
                message = "Cliente atualizado com sucesso",
                data = updatedCustomer
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBooking(string id)
        {
            await _bookingService.DeleteAsync(id);

            return NoContent();
        }
    }
}
