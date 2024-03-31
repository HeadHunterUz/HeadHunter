using HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;

namespace HeadHunter.Services.Services.Experiences;
public interface IExperienceService
{
    Task<ExperienceViewModel> CreateAsync(ExperienceCreateModel experience);
    Task<ExperienceViewModel> UpdateAsync(long id, ExperienceUpdateModel experience);
    Task<bool> DeleteAsync(long id);
    Task<ExperienceViewModel> GetByIdAsync(long id);
    Task<IEnumerable<ExperienceViewModel>> GetAllAsync();
}
