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
    public class CustomerReviewController : ControllerBase
    {
        private readonly ICustomerReviewService _customerReviewService;
        private readonly IValidator<CustomerReviewDto> _validator;

        public CustomerReviewController(ICustomerReviewService customerReviewService, IValidator<CustomerReviewDto> validator)
        {
            _customerReviewService = customerReviewService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersReview()
        {
            var result = await _customerReviewService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerReviewById(string id)
        {
            var result = await _customerReviewService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("reviewHotel/{id}")]
        public async Task<IActionResult> GetReviewHotelAsync(string hotelId)
        {
            var result = await _customerReviewService.GetReviewHotelAsync(hotelId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("reviewRoom/{id}")]
        public async Task<IActionResult> GetReviewRoomAsync(string roomId)
        {
            var result = await _customerReviewService.GetReviewRoomAsync(roomId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerReview([FromBody] CustomerReviewDto customerReviewDto)
        {
            var validationResult = await _validator.ValidateAsync(customerReviewDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var insertedReview = await _customerReviewService.InsertAsync(customerReviewDto);

            return Ok(new
            {
                message = "Feedback cadastrado com sucesso",
                data = insertedReview
            });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerReview(string id, [FromBody] CustomerReviewDto customerReviewDto)
        {
            var validationResult = await _validator.ValidateAsync(customerReviewDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedReview = await _customerReviewService.UpdateAsync(id, customerReviewDto);

            return Ok(new
            {
                message = "Feedback atualizado com sucesso",
                data = updatedReview
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomerReview(string id)
        {
            await _customerReviewService.DeleteAsync(id);

            return NoContent();
        }
    }
}
