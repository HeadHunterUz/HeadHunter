using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
using HeadHunter.Services.Services.Applications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationViewModel>> CreateApplication(ApplicationCreateModel application)
        {
            try
            {
                var createdApplication = await _applicationService.CreateAsync(application);
                return Ok(createdApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationViewModel>> UpdateApplication(long id, ApplicationUpdateModel application)
        {
            try
            {
                var updatedApplication = await _applicationService.UpdateAsync(id, application);
                return Ok(updatedApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteApplication(long id)
        {
            try
            {
                var result = await _applicationService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationViewModel>> GetApplicationById(long id)
        {
            try
            {
                var application = await _applicationService.GetByIdAsync(id);
                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationViewModel>>> GetAllApplications()
        {
            try
            {
                var applications = await _applicationService.GetAllAsync();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}