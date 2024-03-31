//using AutoMapper;
//using HeadHunter.DataAccess;
//using HeadHunter.DataAccess.IRepositories;
//using HeadHunter.Domain.Entities.Core;
//using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
//using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
//using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
//using HeadHunter.Services.Exceptions;
//using HeadHunter.Services.Services.Addresses;

//namespace HeadHunter.Services.Services.Companies;

//public class CompanyService : ICompanyService
//{
//    private IMapper mapper;
//    private IRepository<Company> repository;
//    private IIndustryService industryService;
//    private IAddressService addressService;
//    public readonly string companytable = Constants.CompanyTableName;
//    public readonly string jobvacancytable = Constants.JobVacancyTableName;
//    public readonly string industrytable = Constants.IndustryTableName;

//    public CompanyService(IMapper mapper, IIndustryService industryService, IAddressService addressService, IRepository<Company> repository)
//    {
//        this.mapper = mapper;
//        this.repository = repository;
//        this.industryService = industryService;
//        this.addressService = addressService;
//    }

//    public async Task<CompanyViewModel> CreateAsync(CompanyCreateModel company)
//    {
//        var existIndustry = await industryService.GetByIdAsync(company.IndustryId);
//        var existAddress = await addressService.GetByIdAsync(company.AddressId);

//        var existCompany = (await repository.GetAllAsync(companytable))
//            .FirstOrDefault(a => a.IndustryId == company.IndustryId && a.AddressId == company.AddressId);

//        if (existCompany != null)
//            throw new CustomException(409, "Company is existed");

//        var completed = mapper.Map<Company>(company);
//        await repository.InsertAsync(companytable, completed);

//        var viewModel = mapper.Map<CompanyViewModel>(completed);

//        viewModel.Address = mapper.Map<AddressViewModel>(existAddress);
//        viewModel.Industry = mapper.Map<IndustryViewModel>(existIndustry);

//        return viewModel;
//    }

//    public async Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel company)
//    {
//        var existIndustry = await industryService.GetByIdAsync(company.IndustryId);
//        var existAddress = await addressService.GetByIdAsync(company.AddressId);

//        var existCompany = (await repository.GetByIdAsync(companytable, id))
//            ?? throw new CustomException(404, "Company is not found");
//        var mapped = mapper.Map<CompanyViewModel>(existCompany);

//        mapped.Industry = existIndustry;
//        mapped.Address = existAddress;

//        return mapped;
//    }

//    public async Task<bool> DeleteAsync(long id)
//    {
//        var existCompany = (await repository.GetByIdAsync(companytable, id))
//            ?? throw new CustomException(404, "Company is not found");

//        await repository.DeleteAsync(companytable, id);

//        return true;
//    }

//    public async Task<CompanyViewModel> GetByIdAsync(long id)
//    {
//        var existCompany = (await repository.GetByIdAsync(companytable, id))
//           ?? throw new CustomException(404, "Company is not found");

//        return mapper.Map<CompanyViewModel>(existCompany);
//    }

//    public async Task<IEnumerable<CompanyViewModel>> GetAllAsync()
//    {
//        var Companies = (await repository.GetAllAsync(companytable))
//            .Where(a => !a.IsDeleted);

//        return mapper.Map<IEnumerable<CompanyViewModel>>(Companies);
//    }
//}
