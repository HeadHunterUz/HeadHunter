using HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;
using HeadHunter.Services.Services.Experiences;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/experiences")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpPost]
        public async Task<ActionResult<ExperienceViewModel>> CreateExperience(ExperienceCreateModel experience)
        {
            try
            {
                var createdExperience = await _experienceService.CreateAsync(experience);
                return Ok(createdExperience);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExperienceViewModel>> UpdateExperience(long id, ExperienceUpdateModel experience)
        {
            try
            {
                var updatedExperience = await _experienceService.UpdateAsync(id, experience);
                return Ok(updatedExperience);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteExperience(long id)
        {
            try
            {
                var result = await _experienceService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExperienceViewModel>> GetExperienceById(long id)
        {
            try
            {
                var experience = await _experienceService.GetByIdAsync(id);
                return Ok(experience);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperienceViewModel>>> GetAllExperiences()
        {
            try
            {
                var experiences = await _experienceService.GetAllAsync();
                return Ok(experiences);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}