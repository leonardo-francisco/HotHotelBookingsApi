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
    public class AdditionalServiceController : ControllerBase
    {
        private readonly IAdditionalService _additionalService;
        private readonly IValidator<AdditionalServiceDto> _validator;

        public AdditionalServiceController(IAdditionalService additionalService, IValidator<AdditionalServiceDto> validator)
        {
            _additionalService = additionalService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdditionalServices()
        {
            var result = await _additionalService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdditionalServiceById(string id)
        {
            var result = await _additionalService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("allAdditionalServices/{hotelId}")]
        public async Task<IActionResult> GetByHotelIdAsync(string hotelId)
        {
            var result = await _additionalService.GetByHotelIdAsync(hotelId);

            return Ok(result);
        }

        [HttpGet("availableAdditionalService/{hotelId}")]
        public async Task<IActionResult> GetAvailableServicesAsync(string hotelId)
        {
            var result = await _additionalService.GetAvailableServicesAsync(hotelId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdditionalService([FromBody] AdditionalServiceDto additionalDto)
        {
            var validationResult = await _validator.ValidateAsync(additionalDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
          
            var insertedAdditional = await _additionalService.InsertAsync(additionalDto);                   

            return Ok(new
            {
                message = "Serviço adicional cadastrado com sucesso",
                data = insertedAdditional
            });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdditionalService(string id, [FromBody] AdditionalServiceDto additionalDto)
        {
            var validationResult = await _validator.ValidateAsync(additionalDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }          

            var updatedAdditional = await _additionalService.UpdateAsync(additionalDto.Id, additionalDto);

            return Ok(new
            {
                message = "Serviço adicional atualizado com sucesso",
                data = updatedAdditional
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAdditionalService(string id)
        {
            await _additionalService.DeleteAsync(id);

            return NoContent();
        }
    }
}
