using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;

namespace HeadHunter.Services.Services.Addresses;

public interface IAddressService
{
    /// <summary>
    /// Asynchronously creates a new address based on the provided address creation model.
    /// </summary>
    /// <param name="address">The model containing information for creating the address.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `AddressViewModel` representing the newly created address.</returns>
    Task<AddressViewModel> CreateAsync(AddressCreateModel address);

    /// <summary>
    /// Asynchronously updates an existing address identified by its ID, based on the provided address update model.
    /// </summary>
    /// <param name="id">The unique identifier of the address to be updated.</param>
    /// <param name="address">The model containing updated information for the address.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `AddressViewModel` representing the updated address.</returns>
    Task<AddressViewModel> UpdateAsync(long id, AddressUpdateModel address);

    /// <summary>
    /// Asynchronously deletes an address by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the address to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves an address by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the address to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `AddressViewModel` representing the retrieved address.</returns>
    Task<AddressViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all addresses.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `AddressViewModel`, representing all addresses in the system.</returns>
    Task<IEnumerable<AddressViewModel>> GetAllAsync();

}
