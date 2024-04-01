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
    private IMapper _mapper;
    private IRepository<Industry> _repository;
    private string _industryTable;

    public IndustryService(IMapper mapper, IRepository<Industry> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _industryTable = Constants.IndustryTableName;
    }

    public async Task<IndustryViewModel> CreateAsync(IndustryCreateModel industry)
    {
        var existIndustry = (await _repository.GetAllAsync(_industryTable))
            .FirstOrDefault(i => i.Name == industry.Name);

        if (existIndustry != null)
            throw new CustomException(409, "Industry already exists");

        var mapped = _mapper.Map<Industry>(industry);
        mapped.Id = (await _repository.GetAllAsync(_industryTable)).Last().Id + 1;
        var created = await _repository.InsertAsync(_industryTable, mapped);

        return new IndustryViewModel
        {
            Id = created.Id,
            Name = created.Name,
        };
    }

    public async Task<IndustryViewModel> UpdateAsync(long id, IndustryUpdateModel industry)
    {
        var existIndustry = await _repository.GetByIdAsync(_industryTable, id)
            ?? throw new CustomException(404, "Industry not found");

        if (existIndustry.IsDeleted)
            throw new CustomException(410, "Industry is already deleted");

        var mapped = _mapper.Map(industry, existIndustry);
        var updated = await _repository.UpdateAsync(_industryTable, mapped);

        return new IndustryViewModel
        {
            Id = id,
            Name = updated.Name
        };
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existIndustry = await _repository.GetByIdAsync(_industryTable, id)
            ?? throw new CustomException(404, "Industry not found");

        if (existIndustry.IsDeleted)
            throw new CustomException(410, "Industry is already deleted");

        await _repository.DeleteAsync(_industryTable, id);
        return true;
    }

    public async Task<IndustryViewModel> GetByIdAsync(long id)
    {
        var existIndustry = await _repository.GetByIdAsync(_industryTable, id)
            ?? throw new CustomException(404, "Industry not found");

        if (existIndustry.IsDeleted)
            throw new CustomException(410, "Industry is already deleted");

        return new IndustryViewModel
        {
            Id = existIndustry.Id,
            Name = existIndustry.Name,
        };
    }

    public async Task<IEnumerable<IndustryViewModel>> GetAllAsync()
    {
        var industries = await _repository.GetAllAsync(_industryTable);
        var industryTasks = industries
            .Where(a => !a.IsDeleted)
            .Select(async app =>
            {
                var mapped = _mapper.Map<IndustryViewModel>(app);

                mapped.Id = app.Id;

                return mapped;
            });

        var mappedIndustries = await Task.WhenAll(industryTasks);
        return mappedIndustries;
    }
}