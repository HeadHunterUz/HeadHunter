using HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;

namespace HeadHunter.Services.Services.VacancySkills;

public interface IVacancySkillService
{
    Task<VacancySkillViewModel> CreateAsync(VacancySkillCreateModel vacancySkill);
    Task<VacancySkillViewModel> UpdateAsync(long id, VacancySkillUpdateModel vacancySkill);
    Task<bool> DeleteAsync(long id);
    Task<VacancySkillViewModel> GetByIdAsync(long id);
    Task<IEnumerable<VacancySkillViewModel>> GetAllAsync();
}
