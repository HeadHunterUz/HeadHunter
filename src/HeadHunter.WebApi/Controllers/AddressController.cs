using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.WebApi.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    public class AddressController(IAddressService addressService) : ControllerBase
    {
        private readonly IAddressService addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));

        [HttpPost]
        public async Task<IActionResult> CreateAddress(AddressCreateModel addressCreateModel)
        {
            try
            {
                var createdAddress = await addressService.CreateAsync(addressCreateModel);
                return Ok(createdAddress);
            }
            catch (CustomException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(long id, AddressUpdateModel addressUpdateModel)
        {
            try
            {
                var updatedAddress = await addressService.UpdateAsync(id, addressUpdateModel);
                return Ok(updatedAddress);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(long id)
        {
            var isDeleted = await addressService.DeleteAsync(id);
            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(long id)
        {
            var address = await addressService.GetByIdAsync(id);
            if (address != null)
                return Ok(address);

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await addressService.GetAllAsync();
            return Ok(addresses);
        }
    }
}
namespace HeadHunter.WebApi.Controllers
{
}