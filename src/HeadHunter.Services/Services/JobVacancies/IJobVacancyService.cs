using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

namespace HeadHunter.Services.Services.JobVacancies;

public interface IJobVacancyService
{
    Task<JobVacancyViewModel> CreateAsync(JobVacancyCreateModel vacancy);
    Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateMode vacancy);
    Task<bool> DeleteAsync(long id);
    Task<JobVacancyViewModel> GetByIdAsync(long id);
    Task<IEnumerable<JobVacancyViewModel>> GetAllAsync();
}
