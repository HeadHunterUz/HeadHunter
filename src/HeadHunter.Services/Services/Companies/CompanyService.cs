using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.Industries;

namespace HeadHunter.Services.Services.Companies;
public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Company> _repository;
    private readonly string _companyTable;

    public CompanyService(IMapper mapper, IRepository<Company> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _companyTable = Constants.CompanyTableName;
    }

    public async Task<CompanyViewModel> CreateAsync(CompanyCreateModel company)
    {
        var existCompany = (await _repository.GetAllAsync(_companyTable))
            .FirstOrDefault(a => a.IndustryId == company.IndustryId && a.AddressId == company.AddressId);

        if (existCompany is not null)
            throw new CustomException(409, "Company already exists");
        if (existCompany is not null && existCompany.IsDeleted)
            throw new CustomException(410, "Company is already deleted");

        var mapped = _mapper.Map<Company>(company);
        var last = (await _repository.GetAllAsync(_companyTable)).Last();
        if (last!= null)
            mapped.Id = last.Id + 1;
        else 
            mapped.Id = 1;
        var created = await _repository.InsertAsync(_companyTable, mapped);

        return new CompanyViewModel
        {
            Id = created.Id,
            Name = created.Name,
            Details = created.Details,
            IndustryId = created.IndustryId,
            AddressId = created.AddressId
        };
    }

    public async Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel company)
    {
        var existCompany = await _repository.GetByIdAsync(_companyTable, id)
            ?? throw new CustomException(404, "Company is not found");

        return new CompanyViewModel
        {
            Id = existCompany.Id,
            Name = existCompany.Name,
            Details = existCompany.Details,
            IndustryId = existCompany.IndustryId,
            AddressId = existCompany.AddressId
        };
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCompany = await _repository.GetByIdAsync(_companyTable, id)
            ?? throw new CustomException(404, "Company is not found");

        if (existCompany.IsDeleted)
            throw new CustomException(410, "Company is already deleted");

        await _repository.DeleteAsync(_companyTable, id);
        return true;
    }

    public async Task<CompanyViewModel> GetByIdAsync(long id)
    {
        var existCompany = await _repository.GetByIdAsync(_companyTable, id)
            ?? throw new CustomException(404, "Company is not found");

        if (existCompany.IsDeleted)
            throw new CustomException(410, "Company is already deleted");

        return new CompanyViewModel
        {
            Id = existCompany.Id,
            Name = existCompany.Name,
            IndustryId = existCompany.IndustryId,
            Details = existCompany.Details,
            AddressId = existCompany.AddressId
        };
    }

    public async Task<IEnumerable<CompanyViewModel>> GetAllAsync()
    {
        var companies = await _repository.GetAllAsync(_companyTable);
        var mappedCompanies = _mapper.Map<IEnumerable<CompanyViewModel>>(companies.Where(a => !a.IsDeleted));
        return mappedCompanies;
    }
}