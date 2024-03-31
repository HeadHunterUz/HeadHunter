using HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;

namespace HeadHunter.Services.Services.BasketVacancies;

public interface IBasketVacancyService
{
    /// <summary>
    /// Asynchronously creates a new basket vacancy based on the provided basket vacancy creation model.
    /// </summary>
    /// <param name="basketVacancy">The model containing information for creating the basket vacancy.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `BasketVacancyViewModel` representing the newly created basket vacancy.</returns>
    Task<BasketVacancyViewModel> CreateAsync(BasketVacancyCreateModel basketVacancy);

    /// <summary>
    /// Asynchronously updates an existing basket vacancy identified by its ID, based on the provided basket vacancy update model.
    /// </summary>
    /// <param name="id">The unique identifier of the basket vacancy to be updated.</param>
    /// <param name="basketVacancy">The model containing updated information for the basket vacancy.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `BasketVacancyViewModel` representing the updated basket vacancy.</returns>
    Task<BasketVacancyViewModel> UpdateAsync(long id, BasketVacancyUpdateModel basketVacancy);

    /// <summary>
    /// Asynchronously deletes a basket vacancy by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the basket vacancy to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves a basket vacancy by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the basket vacancy to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a `BasketVacancyViewModel` representing the retrieved basket vacancy.</returns>
    Task<BasketVacancyViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all basket vacancies.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `BasketVacancyViewModel`, representing all basket vacancies in the system.</returns>
    Task<IEnumerable<BasketVacancyViewModel>> GetAllAsync();

}
