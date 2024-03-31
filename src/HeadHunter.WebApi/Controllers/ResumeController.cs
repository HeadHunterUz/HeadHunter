using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;
using HeadHunter.Services.Services.Resumes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/resumes")]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpPost]
        public async Task<ActionResult<ResumeViewModel>> CreateResume(ResumeCreateModel resumeCreateModel)
        {
            try
            {
                var createdResume = await _resumeService.CreateAsync(resumeCreateModel);
                return Ok(createdResume);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResumeViewModel>> UpdateResume(long id, ResumeUpdateModel resumeUpdateModel)
        {
            try
            {
                var updatedResume = await _resumeService.UpdateAsync(id, resumeUpdateModel);
                return Ok(updatedResume);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteResume(long id)
        {
            try
            {
                var deleted = await _resumeService.DeleteAsync(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumeViewModel>>> GetAllResumes()
        {
            try
            {
                var resumes = await _resumeService.GetAllAsync();
                return Ok(resumes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeViewModel>> GetResumeById(long id)
        {
            try
            {
                var resume = await _resumeService.GetByIdAsync(id);
                return Ok(resume);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}