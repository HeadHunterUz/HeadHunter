using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

namespace HeadHunter.Services.Services.JobVacancies;

public class JobVacancyService : IJobVacancyService
{
    public Task<JobVacancyViewModel> CreateAsync(JobVacancyCreateModel company)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JobVacancyViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<JobVacancyViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateMode company)
    {
        throw new NotImplementedException();
    }
}