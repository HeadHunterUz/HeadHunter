using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;

namespace HeadHunter.Services.Services.IndustryCategories;

public interface IIndustryCategoryService
{
    /// <summary>
    /// Asynchronously creates a new industry category based on the provided industry category creation model.
    /// </summary>
    /// <param name="industryCategory">The model containing information for creating the industry category.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `IndustryCategoryViewModel` representing the newly created industry category.</returns>
    Task<IndustryCategoryViewModel> CreateAsync(IndustryCategoryCreateModel industryCategory);

    /// <summary>
    /// Asynchronously updates an existing industry category identified by its ID, based on the provided industry category update model.
    /// </summary>
    /// <param name="id">The unique identifier of the industry category to be updated.</param>
    /// <param name="industryCategory">The model containing updated information for the industry category.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `IndustryCategoryViewModel` representing the updated industry category.</returns>
    Task<IndustryCategoryViewModel> UpdateAsync(long id, IndustryCategoryUpdateModel industryCategory);

    /// <summary>
    /// Asynchronously deletes an industry category by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the industry category to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns a boolean value indicating whether the deletion was successful (`true`) or not (`false`).</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Asynchronously retrieves an industry category by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the industry category to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an `IndustryCategoryViewModel` representing the retrieved industry category.</returns>
    Task<IndustryCategoryViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all industry categories.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, which upon completion returns an enumerable collection of `IndustryCategoryViewModel`, representing all industry categories in the system.</returns>
    Task<IEnumerable<IndustryCategoryViewModel>> GetAllAsync();

}
