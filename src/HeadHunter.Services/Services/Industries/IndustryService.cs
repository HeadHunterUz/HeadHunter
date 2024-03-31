using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Industries;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.IndustryCategories;

namespace HeadHunter.Services.Services.Industries;

public class IndustryService : IIndustryService
{
    private IMapper mapper;
    private IRepository<Industry> repository;
    private IIndustryCategoryService industryCategoryService;
    private string industrytable = Constants.IndustryTableName;
    public IndustryService(IMapper mapper, IRepository<Industry> repository, IndustryCategoryService industryCategoryService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.industryCategoryService = industryCategoryService;
    }
    public async Task<IndustryViewModel> CreateAsync(IndustryCreateModel industry)
    {
        var existCategory = await industryCategoryService.GetByIdAsync(industry.CategoryId);

        var existIndustry = (await repository.GetAllAsync(industrytable))
            .FirstOrDefault(i => i.Name == industry.Name);
        if (existIndustry != null)
            throw new CustomException(409, "Industry is already exists");

        var created = repository.InsertAsync(industrytable, mapper.Map<Industry>(industry));

        return new IndustryViewModel
        {
            Id = created.Id,
            IndustryCategory = existCategory,
            Name = industry.Name,
        };
    }


    public async Task<IndustryViewModel> UpdateAsync(long id, IndustryUpdateModel industry)
    {
        var existCategory = await industryCategoryService.GetByIdAsync(industry.CategoryId);

        var existIndustry = await repository.GetByIdAsync(industrytable, id)
            ?? throw new CustomException(404, "Industry is not found");

        if (existIndustry.IsDeleted)
            throw new CustomException(410, "Industry is already deleted");

        var mapped = mapper.Map(industry, existIndustry);
        var updated = await repository.UpdateAsync(industrytable, mapped);

        return new IndustryViewModel
        {
            Id = id,
            IndustryCategory = existCategory,
            Name = updated.Name
        };
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var existIndustry = await repository.GetByIdAsync(industrytable, id)
            ?? throw new CustomException(404, "Industry is not found");

        if (existIndustry.IsDeleted)
            throw new CustomException(410, "Industry is already deleted");

        await repository.DeleteAsync(industrytable, id);
        return true;
    }
    public async Task<IndustryViewModel> GetByIdAsync(long id)
    {
        var existIndustry = await repository.GetByIdAsync(industrytable, id)
            ?? throw new CustomException(404, "Industry is not found");

        if (existIndustry.IsDeleted)
            throw new CustomException(410, "Industry is already deleted");

        var existCategory = await industryCategoryService.GetByIdAsync(existIndustry.CategoryId);

        return new IndustryViewModel
        {
            Id = existIndustry.Id,
            Name = existIndustry.Name,
            IndustryCategory = existCategory,
        };
    }
}
