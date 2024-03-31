using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;

namespace HeadHunter.Services.Services.Resumes;
public interface IResumeService
{
    /// <summary>
    /// Asynchronously creates a new resume based on the provided resume creation model.
    /// </summary>
    /// <param name="resume">The model containing information for creating the resume.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `ResumeViewModel` representing the newly created resume.</returns>
    Task<ResumeViewModel> CreateAsync(ResumeCreateModel resume);

    /// <summary>
    /// Asynchronously updates an existing resume identified by its ID, based on the provided resume update model.
    /// </summary>
    /// <param name="id">The unique identifier of the resume to be updated.</param>
    /// <param name="resume">The model containing updated information for the resume.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `ResumeViewModel` representing the updated resume.</returns>
    Task<ResumeViewModel> UpdateAsync(long id, ResumeUpdateModel resume);

    /// <summary>
    /// Asynchronously deletes a resume by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the resume to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves a resume by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the resume to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `ResumeViewModel` representing the retrieved resume.</returns>
    Task<ResumeViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all resumes.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `ResumeViewModel`, representing all resumes in the system.</returns>
    Task<IEnumerable<ResumeViewModel>> GetAllAsync();

}
