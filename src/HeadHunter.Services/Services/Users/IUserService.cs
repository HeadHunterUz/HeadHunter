using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.Services.Users;
public interface IUserService
{
    /// <summary>
    /// Asynchronously creates a new user based on the provided user creation model.
    /// </summary>
    /// <param name="user">The model containing information for creating the user.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `UserViewModel` representing the newly created user.</returns>
    Task<UserViewModel> CreateAsync(UserCreateModel user);

    /// <summary>
    /// Asynchronously updates an existing user identified by its ID, based on the provided user update model.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be updated.</param>
    /// <param name="user">The model containing updated information for the user.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `UserViewModel` representing the updated user.</returns>
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user);

    /// <summary>
    /// Asynchronously deletes a user by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves a user by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `UserViewModel` representing the retrieved user.</returns>
    Task<UserViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all users.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `UserViewModel`, representing all users in the system.</returns>
    Task<IEnumerable<UserViewModel>> GetAllAsync();

}
