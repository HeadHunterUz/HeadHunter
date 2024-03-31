using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

namespace HeadHunter.Services.Services.Companies;
public interface ICompanyService
{
    /// <summary>
    /// Asynchronously creates a new company based on the provided company creation model.
    /// </summary>
    /// <param name="company">The model containing information for creating the company.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `CompanyViewModel` representing the newly created company.</returns>
    Task<CompanyViewModel> CreateAsync(CompanyCreateModel company);

    /// <summary>
    /// Asynchronously updates an existing company identified by its ID, based on the provided company update model.
    /// </summary>
    /// <param name="id">The unique identifier of the company to be updated.</param>
    /// <param name="company">The model containing updated information for the company.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `CompanyViewModel` representing the updated company.</returns>
    Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel company);

    /// <summary>
    /// Asynchronously deletes a company by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the company to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves a company by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the company to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `CompanyViewModel` representing the retrieved company.</returns>
    Task<CompanyViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all companies.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `CompanyViewModel`, representing all companies in the system.</returns>
    Task<IEnumerable<CompanyViewModel>> GetAllAsync();

}

