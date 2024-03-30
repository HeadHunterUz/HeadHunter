using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;

namespace HeadHunter.Services.Services.IndustriesCategories;

public interface IIndustryCategoryService
{
    Task<IndustryCategoryViewModel> CreateAsync(IndustryCategoryCreateModel industry);
    Task<IndustryCategoryViewModel> UpdateAsync(long id, IndustryCategoryUpdateModel industry);
    Task<bool> DeleteAsync(long id);
    Task<IndustryCategoryViewModel> GetByIdAsync(long id);
    Task<IEnumerable<IndustryCategoryViewModel>> GetAllAsync();
}
