using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;

namespace HeadHunter.Services.Services.Industries;

public interface IIndustryService
{
    /// <summary>
    /// Asynchronously creates a new industry based on the provided industry creation model.
    /// </summary>
    /// <param name="model">The model containing information for creating the industry.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `IndustryViewModel` representing the newly created industry.</returns>
    Task<IndustryViewModel> CreateAsync(IndustryCreateModel model);

    /// <summary>
    /// Asynchronously updates an existing industry identified by its ID, based on the provided industry update model.
    /// </summary>
    /// <param name="id">The unique identifier of the industry to be updated.</param>
    /// <param name="model">The model containing updated information for the industry.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `IndustryViewModel` representing the updated industry.</returns>
    Task<IndustryViewModel> UpdateAsync(long id, IndustryUpdateModel model);

    /// <summary>
    /// Asynchronously deletes an industry by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the industry to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves an industry by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the industry to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `IndustryViewModel` representing the retrieved industry.</returns>
    Task<IndustryViewModel> GetByIdAsync(long id);
    Task<IEnumerable<IndustryViewModel>> GetAllAsync();
}
