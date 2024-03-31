using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using HeadHunter.Services.Services.Industries;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/industries")]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        [HttpPost]
        public async Task<ActionResult<IndustryViewModel>> CreateIndustry(IndustryCreateModel industryCreateModel)
        {
            try
            {
                var createdIndustry = await _industryService.CreateAsync(industryCreateModel);
                return Ok(createdIndustry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IndustryViewModel>> UpdateIndustry(long id, IndustryUpdateModel industryUpdateModel)
        {
            try
            {
                var updatedIndustry = await _industryService.UpdateAsync(id, industryUpdateModel);
                return Ok(updatedIndustry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteIndustry(long id)
        {
            try
            {
                var deleted = await _industryService.DeleteAsync(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<IndustryViewModel>> GetAllIndustries()
        {
            try
            {
                var industries = _industryService.GetAllAsEnumerableAsync();
                return Ok(industries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IndustryViewModel>> GetIndustryById(long id)
        {
            try
            {
                var industry = await _industryService.GetByIdAsync(id);
                return Ok(industry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}