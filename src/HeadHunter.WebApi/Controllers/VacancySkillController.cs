using HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;
using HeadHunter.Services.Services.VacancySkills;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunter.API.Controllers
{
    [ApiController]
    [Route("api/vacancy-skills")]
    public class VacancySkillController : ControllerBase
    {
        private readonly IVacancySkillService _vacancySkillService;

        public VacancySkillController(IVacancySkillService vacancySkillService)
        {
            _vacancySkillService = vacancySkillService;
        }

        [HttpPost]
        public async Task<ActionResult<VacancySkillViewModel>> CreateVacancySkill(VacancySkillCreateModel vacancySkill)
        {
            try
            {
                var createdVacancySkill = await _vacancySkillService.CreateAsync(vacancySkill);
                return Ok(createdVacancySkill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VacancySkillViewModel>> UpdateVacancySkill(long id, VacancySkillUpdateModel vacancySkill)
        {
            try
            {
                var updatedVacancySkill = await _vacancySkillService.UpdateAsync(id, vacancySkill);
                return Ok(updatedVacancySkill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteVacancySkill(long id)
        {
            try
            {
                var result = await _vacancySkillService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VacancySkillViewModel>> GetVacancySkillById(long id)
        {
            try
            {
                var vacancySkill = await _vacancySkillService.GetByIdAsync(id);
                return Ok(vacancySkill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacancySkillViewModel>>> GetAllVacancySkills()
        {
            try
            {
                var vacancySkills = await _vacancySkillService.GetAllAsync();
                return Ok(vacancySkills);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}