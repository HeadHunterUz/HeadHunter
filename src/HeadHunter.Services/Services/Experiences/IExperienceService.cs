using HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;

namespace HeadHunter.Services.Services.Experiences;
public interface IExperienceService
{
    /// <summary>
    /// Asynchronously creates a new experience based on the provided experience creation model.
    /// </summary>
    /// <param name="experience">The model containing information for creating the experience.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `ExperienceViewModel` representing the newly created experience.</returns>
    Task<ExperienceViewModel> CreateAsync(ExperienceCreateModel experience);

    /// <summary>
    /// Asynchronously updates an existing experience identified by its ID, based on the provided experience update model.
    /// </summary>
    /// <param name="id">The unique identifier of the experience to be updated.</param>
    /// <param name="experience">The model containing updated information for the experience.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `ExperienceViewModel` representing the updated experience.</returns>
    Task<ExperienceViewModel> UpdateAsync(long id, ExperienceUpdateModel experience);

    /// <summary>
    /// Asynchronously deletes an experience by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the experience to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves an experience by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the experience to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `ExperienceViewModel` representing the retrieved experience.</returns>
    Task<ExperienceViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all experiences.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `ExperienceViewModel`, representing all experiences in the system.</returns>
    Task<IEnumerable<ExperienceViewModel>> GetAllAsync();

}
