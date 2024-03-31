using HeadHunter.Services.DTOs.Admins.Dtos;

namespace HeadHunter.Services.Services.Admins;

public interface IAdminService
{
    /// <summary>
    /// Asynchronously creates a new admin based on the provided admin creation model.
    /// </summary>
    /// <param name="model">The model containing information for creating the admin.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `AdminViewModel` representing the newly created admin.</returns>
    Task<AdminViewModel> CreateAsync(AdminCreateModel model);

    /// <summary>
    /// Asynchronously updates an existing admin identified by its ID, based on the provided admin update model.
    /// </summary>
    /// <param name="id">The unique identifier of the admin to be updated.</param>
    /// <param name="model">The model containing updated information for the admin.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `AdminViewModel` representing the updated admin.</returns>
    Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel model);

    /// <summary>
    /// Asynchronously deletes an admin by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the admin to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all admins.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `AdminViewModel`, representing all admins in the system.</returns>
    Task<IEnumerable<AdminViewModel>> GetAllAsync();

    /// <summary>
    /// Asynchronously retrieves an admin by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the admin to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `AdminViewModel` representing the retrieved admin.</returns>
    Task<AdminViewModel> GetByIdAsync(long id);

}
