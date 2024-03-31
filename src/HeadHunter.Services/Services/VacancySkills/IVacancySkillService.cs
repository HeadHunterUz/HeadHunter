using HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;

namespace HeadHunter.Services.Services.VacancySkills;

public interface IVacancySkillService
{
    Task<VacancySkillViewModel> CreateVacancySkillAsync(VacancySkillCreateModel vacancySkill);
    Task<VacancySkillViewModel> UpdateSkillAsync(long id, VacancySkillUpdateModel vacancySkill);
    Task<bool> CreateVacancySkillAsync(long id);
    Task<VacancySkillViewModel> GetByIdAsync(long id);
    Task<IEnumerable<VacancySkillViewModel>> GetAllAsync();
}
