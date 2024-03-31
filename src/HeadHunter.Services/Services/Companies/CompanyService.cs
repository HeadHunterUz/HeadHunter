using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.Industries;
using HeadHunter.Services.Services.JobVacancies;
using HeadHunter.Services.Services.Users;
using System.Runtime.InteropServices;

namespace HeadHunter.Services.Services.Companies;

public class CompanyService : ICompanyService
{
    private IMapper mapper;
    private IRepository<Company> repository;
    private IIndustryService industryService;
    private IAddressService addressService;
    public readonly string companyTable = Constants.CompanyTableName;
    public readonly string jobvacancytable = Constants.JobVacancyTableName;
    public readonly string industrytable = Constants.IndustryTableName;

    public CompanyService(IMapper mapper, IIndustryService industryService, IAddressService addressService, IRepository<Company> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.industryService = industryService;
        this.addressService = addressService;
    }

    
    public async Task<CompanyViewModel> CreateAsync(CompanyCreateModel company)
    {
        var existIndustry = await industryService.GetByIdAsync(company.IndustryId);
        var existAddress = await addressService.GetByIdAsync(company.AddressId);

        var existCompany = (await repository.GetAllAsync(companyTable))
            .FirstOrDefault(a => a.IndustryId == company.IndustryId && a.AddressId == company.AddressId);

        if (existCompany != null)
            throw new CustomException(409, "Company already exists");
        if (existCompany.IsDeleted)
            throw new CustomException(410, "Company is already deleted");

        var mapped = mapper.Map<Company>(company);
        var created = await repository.InsertAsync(companyTable, mapped);

        return new CompanyViewModel
        {
            Id = created.Id,
            Name = created.Name,
            Details = created.Details,
            Address = existAddress,
            Industry = existIndustry,
        };
    }


    public async Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel company)
    {
        var existIndustry = await industryService.GetByIdAsync(company.IndustryId);
        var existAddress = await addressService.GetByIdAsync(company.AddressId);

        var existCompany = (await repository.GetByIdAsync(companyTable, id))
            ?? throw new CustomException(404, "Company is not found");

        return new CompanyViewModel
        {
            Id = existCompany.Id,
            Address = existAddress,
            Industry = existIndustry,
            Details = existCompany.Details,
            Name = existCompany.Name
        };
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existCompany = (await repository.GetByIdAsync(companyTable, id))
            ?? throw new CustomException(404, "Company is not found");
        if (existCompany.IsDeleted)
            throw new CustomException(410, "Company is already deleted");

        await repository.DeleteAsync(companyTable, id);

        return true;
    }


    public async Task<CompanyViewModel> GetByIdAsync(long id)
    {
        var existCompany = (await repository.GetByIdAsync(companyTable, id))
           ?? throw new CustomException(404, "Company is not found");

        if (existCompany.IsDeleted)
            throw new CustomException(410, "Company is already deleted");
        var existIndustry = await industryService.GetByIdAsync(existCompany.IndustryId);
        var existAddress = await addressService.GetByIdAsync(existCompany.AddressId);

        return mapper.Map<CompanyViewModel>(existCompany);
    }


    public async Task<IEnumerable<CompanyViewModel>> GetAllAsync()
    {
        var companies = await repository.GetAllAsync(companyTable);
        var companiesTasks = companies
            .Where(a => !a.IsDeleted)
            .Select(async app =>
            {
                var existIndustryTask = industryService.GetByIdAsync(app.IndustryId);
                var existAddressTask = addressService.GetByIdAsync(app.AddressId);
                var mapped = mapper.Map<CompanyViewModel>(app);

                mapped.Id = app.Id;

                var existIndustry = await existIndustryTask ?? new IndustryViewModel();
                var existAddress = await existAddressTask ?? new AddressViewModel();

                mapped.Industry = existIndustry;
                mapped.Address = existAddress;

                return mapped;
            });

        var mappedCompanies = await Task.WhenAll(companiesTasks);
        return mappedCompanies;
    }
}
