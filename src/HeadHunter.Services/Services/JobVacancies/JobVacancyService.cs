using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.Companies;
using HeadHunter.Services.Services.Jobs;

namespace HeadHunter.Services.Services.JobVacancies;

public class JobVacancyService : IJobVacancyService
{
    private IMapper mapper;
    private IRepository<JobVacancy> repository;
    private IJobService jobService;
    private ICompanyService companyService;
    private IAddressService addressService;
    private readonly string vacancyTable = Constants.JobVacancyTableName;

    public JobVacancyService(
        IMapper mapper,
        IRepository<JobVacancy> repository,
        IJobService jobService,
        ICompanyService companyService,
        IAddressService addressService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.jobService = jobService;
        this.companyService = companyService;
        this.addressService = addressService;
    }


    public async Task<JobVacancyViewModel> CreateAsync(JobVacancyCreateModel vacancy)
    {
        var existJob = await jobService.GetByIdAsync(vacancy.JobId);
        var existAddress = await addressService.GetByIdAsync(vacancy.AddressId);
        var existCompany = await addressService.GetByIdAsync(vacancy.CompanyId);

        var existVacancy = (await repository.GetAllAsync(vacancyTable))
            .Where(v => v.JobId == existJob.Id)
            ?? throw new CustomException(409, "Vacancy is already exist");

        var created = mapper.Map<JobVacancy>(vacancy);
        created.Id = await GenerateNewId(); 
        await repository.InsertAsync(vacancyTable, created);

        return mapper.Map<JobVacancyViewModel>(vacancy);

    }

    public async Task<long> GenerateNewId()
    {
        long maxId = (await repository.GetAllAsync(vacancyTable)).Max(v => v.Id);
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existVacancy = await repository.GetByIdAsync(vacancyTable, id)
            ?? throw new CustomException(404, "Vacancy is not found");

        await repository.DeleteAsync(vacancyTable, id);

        return true;
    }

    public async Task<IEnumerable<JobVacancyViewModel>> GetAllAsync()
    {
        var Vacancies = await repository.GetAllAsync(vacancyTable);
        return mapper.Map<IEnumerable<JobVacancyViewModel>>(Vacancies);
    }

    public async Task<JobVacancyViewModel> GetByIdAsync(long id)
    {
        var existVacancy = await repository.GetByIdAsync(vacancyTable, id)
           ?? throw new CustomException(404, "Vacancy is not found");

        return mapper.Map<JobVacancyViewModel>(existVacancy);
    }

    public async Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateModel vacancy)
    {
        var existJob = await jobService.GetByIdAsync(vacancy.JobId);
        var existAddress = await addressService.GetByIdAsync(vacancy.AddressId);
        var existCompany = await addressService.GetByIdAsync(vacancy.CompanyId);

        var existVacancy = await repository.GetByIdAsync(vacancyTable, id)
           ?? throw new CustomException(404, "Vacancy is not found");

        var mappedVacancy = mapper.Map(vacancy, existVacancy);
        var updatedVacancy = repository.UpdateAsync(vacancyTable, mappedVacancy);

        return mapper.Map<JobVacancyViewModel>(updatedVacancy);
    }
}