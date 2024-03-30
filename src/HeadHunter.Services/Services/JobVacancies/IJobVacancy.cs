using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

namespace HeadHunter.Services.Services.JobVacancies;

public interface IJobVacancy
{
    Task<JobVacancyViewModel> CreateAsync(JobVacancyUpdateModel company);
    Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateModel company);
    Task<bool> DeleteAsync(long id);
    Task<JobVacancyViewModel> GetByIdAsync(long id);
    Task<IEnumerable<JobVacancyViewModel>> GetAllAsync();
}
