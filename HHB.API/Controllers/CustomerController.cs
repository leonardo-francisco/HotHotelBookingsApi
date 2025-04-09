using FluentValidation;
using HHB.Application.Contracts;
using HHB.Application.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HHB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerDto> _validator;

        public CustomerController(ICustomerService customerService, IValidator<CustomerDto> validator)
        {
            _customerService = customerService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var result = await _customerService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var result = await _customerService.GetByEmailAsync(email);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(customerDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

               var insertedCustomer = await _customerService.InsertAsync(customerDto);

                return Ok(new 
                { 
                    message = "Cliente cadastrado com sucesso",
                    data = insertedCustomer
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] CustomerDto customerDto)
        {
            var validationResult = await _validator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
           
            var updatedCustomer = await _customerService.UpdateAsync(id, customerDto);

            return Ok(new
            {
                message = "Cliente atualizado com sucesso",
                data = updatedCustomer
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomer(string id)
        {           
            await _customerService.DeleteAsync(id);

            return NoContent();
        }
    }
}
