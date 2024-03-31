using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Core;
using HeadHunter.Services.Services.Jobs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        public async Task<ActionResult<JobViewModel>> CreateJob(JobCreateModel jobCreateModel)
        {
            try
            {
                var createdJob = await _jobService.CreateAsync(jobCreateModel);
                return Ok(createdJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobViewModel>> UpdateJob(long id, JobUpdateModel jobUpdateModel)
        {
            try
            {
                var updatedJob = await _jobService.UpdateAsync(id, jobUpdateModel);
                return Ok(updatedJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteJob(long id)
        {
            try
            {
                var deleted = await _jobService.DeleteAsync(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobViewModel>> GetAllJobs()
        {
            try
            {
                var jobs = _jobService.GetAllAsEnumerable();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobViewModel>> GetJobById(long id)
        {
            try
            {
                var job = await _jobService.GetByIdAsync(id);
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}