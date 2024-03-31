using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

namespace HeadHunter.Services.Services.JobVacancies;

public interface IJobVacancyService
{
    /// <summary>
    /// Asynchronously creates a new job vacancy based on the provided job vacancy creation model.
    /// </summary>
    /// <param name="vacancy">The model containing information for creating the job vacancy.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `JobVacancyViewModel` representing the newly created job vacancy.</returns>
    Task<JobVacancyViewModel> CreateAsync(JobVacancyCreateModel vacancy);

    /// <summary>
    /// Asynchronously updates an existing job vacancy identified by its ID, based on the provided job vacancy update model.
    /// </summary>
    /// <param name="id">The unique identifier of the job vacancy to be updated.</param>
    /// <param name="vacancy">The model containing updated information for the job vacancy.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `JobVacancyViewModel` representing the updated job vacancy.</returns>
    Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateModel vacancy);

    /// <summary>
    /// Asynchronously deletes a job vacancy by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the job vacancy to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves a job vacancy by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the job vacancy to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `JobVacancyViewModel` representing the retrieved job vacancy.</returns>
    Task<JobVacancyViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all job vacancies.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `JobVacancyViewModel`, representing all job vacancies in the system.</returns>
    Task<IEnumerable<JobVacancyViewModel>> GetAllAsync();

}
