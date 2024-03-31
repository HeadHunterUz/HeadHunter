using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.Services.JobVacancies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/job-vacancies")]
    public class JobVacancyController : ControllerBase
    {
        private readonly IJobVacancyService _jobVacancyService;

        public JobVacancyController(IJobVacancyService jobVacancyService)
        {
            _jobVacancyService = jobVacancyService;
        }

        [HttpPost]
        public async Task<ActionResult<JobVacancyViewModel>> CreateJobVacancy(JobVacancyCreateModel vacancy)
        {
            try
            {
                var createdJobVacancy = await _jobVacancyService.CreateAsync(vacancy);
                return Ok(createdJobVacancy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobVacancyViewModel>> UpdateJobVacancy(long id, JobVacancyUpdateModel vacancy)
        {
            try
            {
                var updatedJobVacancy = await _jobVacancyService.UpdateAsync(id, vacancy);
                return Ok(updatedJobVacancy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteJobVacancy(long id)
        {
            try
            {
                var result = await _jobVacancyService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobVacancyViewModel>> GetJobVacancyById(long id)
        {
            try
            {
                var jobVacancy = await _jobVacancyService.GetByIdAsync(id);
                return Ok(jobVacancy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobVacancyViewModel>>> GetAllJobVacancies()
        {
            try
            {
                var jobVacancies = await _jobVacancyService.GetAllAsync();
                return Ok(jobVacancies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}