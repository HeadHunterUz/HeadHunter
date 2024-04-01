using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Core;

namespace HeadHunter.Services.Services.Jobs;

public interface IJobService
{
    /// <summary>
    /// Asynchronously creates a new job based on the provided job creation model.
    /// </summary>
    /// <param name="model">The model containing information for creating the job.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `JobViewModel` representing the newly created job.</returns>
    Task<JobViewModel> CreateAsync(JobCreateModel model);

    /// <summary>
    /// Asynchronously updates an existing job identified by its ID, based on the provided job update model.
    /// </summary>
    /// <param name="id">The unique identifier of the job to be updated.</param>
    /// <param name="model">The model containing updated information for the job.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `JobViewModel` representing the updated job.</returns>
    Task<JobViewModel> UpdateAsync(long id, JobUpdateModel model);

    /// <summary>
    /// Asynchronously deletes a job by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the job to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves a job by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the job to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `JobViewModel` representing the retrieved job.</returns>
    Task<JobViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Retrieves all jobs as an enumerable collection.
    /// </summary>
    /// <returns>An enumerable collection of `JobViewModel`, representing all jobs in the system.</returns>
    IEnumerable<JobViewModel> GetAllAsEnumerable();
}
