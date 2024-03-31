using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Domain.Entities.Industries;
using HeadHunter.Services.DTOs.Admins.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Services.Services.Industries;

public class IndustryService : IIndustryService
{
    private IMapper mapper;
    private List<Industry> industries;
    private IRepository<Industry> repository;
    private string table = Constants.IndustryTableName;
    public IndustryService(IMapper mapper, IRepository<Industry> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.industries = repository.GetAllAsync(table).Result.ToList();
    }
    public async Task<IndustryViewModel> CreateAsync(IndustryCreateModel model)
    {
        var industry = mapper.Map<Industry>(model);
        industries.Add(industry);
        await repository.InsertAsync(table, industry);
        return mapper.Map<IndustryViewModel>(model);
    }
    public async Task<IndustryViewModel> UpdateAsync(long id, IndustryUpdateModel model)
    {
        var exist = industries.FirstOrDefault(x => x.Id == id);
        if (exist is null)
            throw new Exception("This industry is not found");

        exist.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(table, exist);
        return mapper.Map<IndustryViewModel>(exist);
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var exist = industries.FirstOrDefault(x => x.Id == id);
        if (exist is null)
            throw new Exception("This industry is not found");

        exist.DeletedAt = DateTime.UtcNow;
        industries.Remove(exist);
        await repository.DeleteAsync(table, id);
        return true;
    }
    public async Task<IndustryViewModel> GetByIdAsync(long id)
    {
        var exist = industries.FirstOrDefault(x => x.Id == id);
        if (exist is null)
            throw new Exception("This industry is not found");

        return await Task.FromResult(mapper.Map<IndustryViewModel>(exist));
    }
    public IEnumerable<IndustryViewModel> GetAllAsEnumerableAsync()
    {
        return mapper.Map<IEnumerable<IndustryViewModel>>(industries);
    }
    public IQueryable<IndustryViewModel> GetAllAsQueryable()
    {
        return mapper.Map<IQueryable<IndustryViewModel>>(industries);
    }
}
