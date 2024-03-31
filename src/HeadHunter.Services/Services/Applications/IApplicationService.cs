using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;

namespace HeadHunter.Services.Services.Applications;

public interface IApplicationService
{
    /// <summary>
    /// Asynchronously creates a new application based on the provided application creation model.
    /// </summary>
    /// <param name="application">The model containing information for creating the application.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `ApplicationViewModel` representing the newly created application.</returns>
    Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application);

    /// <summary>
    /// Asynchronously updates an existing application identified by its ID, based on the provided application update model.
    /// </summary>
    /// <param name="id">The unique identifier of the application to be updated.</param>
    /// <param name="application">The model containing updated information for the application.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `ApplicationViewModel` representing the updated application.</returns>
    Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel application);

    /// <summary>
    /// Asynchronously deletes an application by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the application to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves an application by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the application to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `ApplicationViewModel` representing the retrieved application.</returns>
    Task<ApplicationViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all applications.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `ApplicationViewModel`, representing all applications in the system.</returns>
    Task<IEnumerable<ApplicationViewModel>> GetAllAsync();

}
