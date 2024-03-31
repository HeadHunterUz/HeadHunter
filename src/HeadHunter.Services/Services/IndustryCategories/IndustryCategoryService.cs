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
    public readonly string industrycategorytable = Constants.IndustryCategoryTableName;


    public IndustryCategoryService(IMapper mapper, IRepository<IndustryCategory> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }


    public async Task<IndustryCategoryViewModel> CreateAsync(IndustryCategoryCreateModel industryCategory)
    {
        var existIndustryCategory = (await repository.GetAllAsync(industrycategorytable))
            .FirstOrDefault(u => u.Name.ToLower() == industryCategory.Name.ToLower());

        if (existIndustryCategory != null)
            throw new CustomException(409, "IndustryCategory already exists");


        var created = await repository.InsertAsync(industrycategorytable, mapper.Map<IndustryCategory>(industryCategory));

        return new IndustryCategoryViewModel
        {
            Id = created.Id,
            Name = created.Name,
            ParentId = created.ParentId,
        };
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existIndustryCategory = await repository.GetByIdAsync(industrycategorytable, id)
             ?? throw new CustomException(404, "Industry Category not found");

        if (existIndustryCategory.IsDeleted)
            throw new CustomException(410, "Industry Category is already deleted");

        await repository.DeleteAsync(industrycategorytable, id);

        return true;
    }


    public async Task<IEnumerable<IndustryCategoryViewModel>> GetAllAsync()
    {
        var industryCategories = await repository.GetAllAsync(industrycategorytable);
        var industrycategoriesTasks = industryCategories
            .Where(a => !a.IsDeleted)
            .Select(async app =>
            {
                var mapped = mapper.Map<IndustryCategoryViewModel>(app);
                mapped.Id = app.Id;
                return mapped;
            });

        var mappedIndustryCategories = await Task.WhenAll(industrycategoriesTasks);
        return mappedIndustryCategories;
    }


    public async Task<IndustryCategoryViewModel> GetByIdAsync(long id)
    {
        var existIndustryCategory = await repository.GetByIdAsync(industrycategorytable, id)
           ?? throw new CustomException(404, "Inustry Category not found");

        if (existIndustryCategory.IsDeleted)
            throw new CustomException(410, "Industry Category is already deleted");

        return mapper.Map<IndustryCategoryViewModel>(existIndustryCategory);
    }


    public async Task<IndustryCategoryViewModel> UpdateAsync(long id, IndustryCategoryUpdateModel industryCategory)
    {
        var existIndustryCategory = await repository.GetByIdAsync(industrycategorytable, id)
             ?? throw new CustomException(404, "Industry Category not found");

        var mapped = mapper.Map(industryCategory, existIndustryCategory);

        var updated = await repository.UpdateAsync(industrycategorytable, mapped);

        return mapper.Map<IndustryCategoryViewModel>(mapped);
    }
}
