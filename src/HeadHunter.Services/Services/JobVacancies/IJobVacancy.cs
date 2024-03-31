using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

namespace HeadHunter.Services.Services.JobVacancies;

public interface IJobVacancy
{
    Task<JobVacancyViewModel> CreateAsync(JobVacancyCreateModel company);
    Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateMode company);
    Task<bool> DeleteAsync(long id);
    Task<JobVacancyViewModel> GetByIdAsync(long id);
    Task<IEnumerable<JobVacancyViewModel>> GetAllAsync();
}
