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
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IValidator<HotelDto> _validator;

        public HotelController(IHotelService hotelService, IValidator<HotelDto> validator)
        {
            _hotelService = hotelService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var result = await _hotelService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(string id)
        {
            var result = await _hotelService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetHotelByEmail(string email)
        {
            var result = await _hotelService.GetByEmailAsync(email);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDto hotelDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(hotelDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var insertedHotel = await _hotelService.InsertAsync(hotelDto);

                return Ok(new
                {
                    message = "Hotel cadastrado com sucesso",
                    data = insertedHotel
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(string id, [FromBody] HotelDto hotelDto)
        {
            var validationResult = await _validator.ValidateAsync(hotelDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedHotel = await _hotelService.UpdateAsync(id, hotelDto);

            return Ok(new
            {
                message = "Hotel atualizado com sucesso",
                data = updatedHotel
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveHotel(string id)
        {
            await _hotelService.DeleteAsync(id);

            return NoContent();
        }
    }
}
