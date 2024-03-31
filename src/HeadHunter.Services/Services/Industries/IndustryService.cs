using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Industries;
using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.IndustryCategories;
using HeadHunter.Services.Services.JobVacancies;
using HeadHunter.Services.Services.Users;

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
        var mapped = mapper.Map<Industry>(industry);
        mapped.Id = (await repository.GetAllAsync(industrytable)).Last().Id + 1;
        var created = repository.InsertAsync(industrytable, mapped);

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

    public async Task<IEnumerable<IndustryViewModel>> GetAllAsync()
    {
        var industries = await repository.GetAllAsync(industrytable);
        var industryTasks = industries
            .Where(a => !a.IsDeleted)
            .Select(async app =>
            {
                var existCategoryTask = industryCategoryService.GetByIdAsync(app.CategoryId);
                var mapped = mapper.Map<IndustryViewModel>(app);

                mapped.Id = app.Id;

                var existCategory = await existCategoryTask ?? new IndustryCategoryViewModel();

                mapped.IndustryCategory = existCategory;

                return mapped;
            });

        var mappedApplications = await Task.WhenAll(industryTasks);
        return mappedApplications;
    }
}
