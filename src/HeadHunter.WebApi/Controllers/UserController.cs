using AutoMapper;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.Industries;
using HeadHunter.Services.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIndustryService _industryService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IIndustryService industryService, IAddressService addressService, IMapper mapper)
        {
            _userService = userService;
            _industryService = industryService;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserCreateModel userCreateModel)
        {
            try
            {
                var createdUser = await _userService.CreateAsync(userCreateModel);
                return Ok(createdUser);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserViewModel>> UpdateUser(long id, UserUpdateModel userUpdateModel)
        {
            try
            {
                var updatedUser = await _userService.UpdateAsync(id, userUpdateModel);
                return Ok(updatedUser);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(long id)
        {
            try
            {
                var deleted = await _userService.DeleteAsync(id);
                return Ok(deleted);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUserById(long id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}