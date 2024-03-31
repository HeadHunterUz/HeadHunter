// using AutoMapper;
// using HeadHunter.DataAccess;
// using HeadHunter.DataAccess.IRepositories;
// using HeadHunter.Domain.Entities.Jobs;
// using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
// using HeadHunter.Services.Exceptions;
// using HeadHunter.Services.Services.Addresses;
// using HeadHunter.Services.Services.Companies;

// namespace HeadHunter.Services.Services.JobVacancies;

// public class JobVacancyService : IJobVacancyService
// {
//     private IMapper mapper;
//     private IRepository<JobVacancy> repository;
//     private IJobService jobService;
//     private ICompanyService companyService;
//     private IAddressService addressService;
//     private readonly string vacancytable = Constants.JobVacancyTableName;

//     public JobVacancyService(
//         IMapper mapper,
//         IRepository<JobVacancy> repository,
//         IJobService jobService,
//         ICompanyService companyService,
//         IAddressService addressService)
//     {
//         this.mapper = mapper;
//         this.repository = repository;
//         this.jobService = jobService;
//         this.companyService = companyService;
//         this.addressService = addressService;
//     }


//     public async Task<JobVacancyViewModel> CreateAsync(JobVacancyCreateModel vacancy)
//     {
//         var existJob = await jobService.GetByIdAsync(vacancy.JobId);
//         var existAddress = await addressService.GetByIdAsync(vacancy.AddressId);
//         var existCompany = await addressService.GetByIdAsync(vacancy.CompanyId);

//         var existVacancy = (await repository.GetAllAsync(vacancytable))
//             .Where(v => v.JobId == existJob.JobId)
//             ?? throw new CustomException(409, "Vacancy is already exist");

//         var created = mapper.Map<JobVacancy>(vacancy);
//         await repository.InsertAsync(vacancytable, created);

//         return mapper.Map<JobVacancyViewModel>(vacancy);

//     }

//     public async Task<bool> DeleteAsync(long id)
//     {
//         var existVacancy = await repository.GetByIdAsync(vacancytable, id)
//             ?? throw new CustomException(404, "Vacancy is not found");

//         await repository.DeleteAsync(vacancytable, id);

//         return true;
//     }

//     public async Task<IEnumerable<JobVacancyViewModel>> GetAllAsync()
//     {
//         var Vacancies = await repository.GetAllAsync(vacancytable);
//         return mapper.Map<IEnumerable<JobVacancyViewModel>>(Vacancies);
//     }

//     public async Task<JobVacancyViewModel> GetByIdAsync(long id)
//     {
//         var existVacancy = await repository.GetByIdAsync(vacancytable, id)
//            ?? throw new CustomException(404, "Vacancy is not found");

//         return mapper.Map<JobVacancyViewModel>(existVacancy);
//     }

//     public async Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateModel vacancy)
//     {
//         var existJob = await jobService.GetByIdAsync(vacancy.JobId);
//         var existAddress = await addressService.GetByIdAsync(vacancy.AddressId);
//         var existCompany = await addressService.GetByIdAsync(vacancy.CompanyId);

//         var existVacancy = await repository.GetByIdAsync(vacancytable, id)
//            ?? throw new CustomException(404, "Vacancy is not found");

//         var mappedVacancy = mapper.Map(vacancy, existVacancy);
//         var updatedVacancy = repository.UpdateAsync(vacancytable, mappedVacancy);

//         return mapper.Map<JobVacancyViewModel>(updatedVacancy);
//     }
// }