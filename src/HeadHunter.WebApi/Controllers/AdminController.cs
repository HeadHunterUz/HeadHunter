using HeadHunter.Services.DTOs.Admins.Dtos;
using HeadHunter.Services.Services.Admins;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<ActionResult<AdminViewModel>> CreateAdmin(AdminCreateModel adminCreateModel)
        {
            try
            {
                var createdAdmin = await _adminService.CreateAsync(adminCreateModel);
                return Ok(createdAdmin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminViewModel>> UpdateAdmin(long id, AdminUpdateModel adminUpdateModel)
        {
            try
            {
                var updatedAdmin = await _adminService.UpdateAsync(id, adminUpdateModel);
                return Ok(updatedAdmin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAdmin(long id)
        {
            try
            {
                var deleted = await _adminService.DeleteAsync(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdminViewModel>> GetAllAdmins()
        {
            try
            {
                var admins = _adminService.GetAllAsync();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminViewModel>> GetAdminById(long id)
        {
            try
            {
                var admin = await _adminService.GetByIdAsync(id);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}