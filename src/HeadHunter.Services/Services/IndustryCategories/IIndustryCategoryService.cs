using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;

namespace HeadHunter.Services.Services.IndustryCategories;

public interface IIndustryCategoryService
{
    Task<IndustryCategoryViewModel> CreateAsync(IndustryCategoryCreateModel industryCategory);
    Task<IndustryCategoryViewModel> UpdateAsync(long id, IndustryCategoryUpdateModel industryCategory);
    Task<bool> DeleteAsync(long id);
    Task<IndustryCategoryViewModel> GetByIdAsync(long id);
    Task<IEnumerable<IndustryCategoryViewModel>> GetAllAsync();
}
