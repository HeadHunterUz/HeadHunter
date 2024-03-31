using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Industries;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
using HeadHunter.Services.Exceptions;

namespace HeadHunter.Services.Services.IndustryCategories;

public class IndustryCategoryService : IIndustryCategoryService
{
    private readonly IMapper mapper;
    private readonly IRepository<IndustryCategory> repository;
    public readonly string table = Constants.IndustryCategoryTableName;

    public IndustryCategoryService(IMapper mapper, IRepository<IndustryCategory> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<IndustryCategoryViewModel> CreateAsync(IndustryCategoryCreateModel industryCategory)
    {
        var existIndustryCategory = (await repository.GetAllAsync(table))
            .FirstOrDefault(u => u.Name.ToLower() == industryCategory.Name.ToLower());

        if (existIndustryCategory != null)
            throw new CustomException(409, "IndustryCategory already exists");

        var createdIndustryCategory = mapper.Map<IndustryCategory>(industryCategory);
        createdIndustryCategory.Id = await GenerateNewId(); // Set the ID to a new generated ID
        await repository.InsertAsync(table, createdIndustryCategory);

        return mapper.Map<IndustryCategoryViewModel>(createdIndustryCategory);
    }

    private async Task<long> GenerateNewId()
    {
        var existingIndustryCategories = await repository.GetAllAsync(table);
        long maxId = existingIndustryCategories.Any() ? existingIndustryCategories.Max(ic => ic.Id) : 0;
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existIndustryCategory = await repository.GetByIdAsync(table, id)
             ?? throw new CustomException(404, "Industry Category not found");

        await repository.DeleteAsync(table, id);

        return true;
    }

    public async Task<IEnumerable<IndustryCategoryViewModel>> GetAllAsync()
    {
        var industryCategories = await repository.GetAllAsync(table);

        return mapper.Map<IEnumerable<IndustryCategoryViewModel>>(industryCategories);
    }

    public async Task<IndustryCategoryViewModel> GetByIdAsync(long id)
    {
        var existIndustryCategory = await repository.GetByIdAsync(table, id)
           ?? throw new CustomException(404, "Inustry Category not found");

        return mapper.Map<IndustryCategoryViewModel>(existIndustryCategory);
    }

    public async Task<IndustryCategoryViewModel> UpdateAsync(long id, IndustryCategoryUpdateModel industryCategory)
    {
        var existIndustryCategory = await repository.GetByIdAsync(table, id)
             ?? throw new CustomException(404, "Industry Category not found");

        var mappedIndustryCategory = mapper.Map(industryCategory, existIndustryCategory);

        await repository.UpdateAsync(table, mappedIndustryCategory);

        return mapper.Map<IndustryCategoryViewModel>(mappedIndustryCategory);
    }
}
