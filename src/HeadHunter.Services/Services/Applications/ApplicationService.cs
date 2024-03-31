// using AutoMapper;
// using HeadHunter.DataAccess;
// using HeadHunter.DataAccess.IRepositories;
// using HeadHunter.Domain.Entities.Core;
// using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
// using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
// using HeadHunter.Services.DTOs.Users.Dtos;
// using HeadHunter.Services.Exceptions;
// using HeadHunter.Services.Services.JobVacancies;

// namespace HeadHunter.Services.Services.Applications;

// public class ApplicationService : IApplicationService
// {
//    private IMapper mapper;
//    private IRepository<Application> repository;
//    private IUserService userService;
//    private IJobVacancyService jobVacancyService;
//    public readonly string applicationtable = Constants.ApplicationTableName;
//    public readonly string jobvacancytable = Constants.JobVacancyTableName;

//    public ApplicationService(IMapper mapper, IUserService userService, IJobVacancyService jobVacancy, IRepository<Application> repository)
//    {
//        this.mapper = mapper;
//        this.repository = repository;
//        this.userService = userService;
//        this.jobVacancyService = jobVacancy;
//    }


//    public async Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application)
//    {
//        var existUser = await userService.GetByIdAsync(application.UserId);
//        var existJobVacancy = await jobVacancyService.GetByIdAsync(application.VacancyId);

//        var existApplication = (await repository.GetAllAsync(applicationtable))
//            .FirstOrDefault(a => a.UserId == application.UserId && a.VacancyId == application.VacancyId);

//        if (existApplication != null)
//            throw new CustomException(409, "You already applied");

//        var completed = mapper.Map<Application>(application);
//        await repository.InsertAsync(applicationtable, completed);

//        var viewModel = mapper.Map<ApplicationViewModel>(completed);

//        viewModel.JobVacancy = mapper.Map<JobVacancyViewModel>(existJobVacancy);
//        viewModel.User = mapper.Map<UserViewModel>(existUser);

//        return viewModel;
//    }


//    public async Task<bool> DeleteAsync(long id)
//    {
//        var existApplication = (await repository.GetByIdAsync(applicationtable, id))
//            ?? throw new CustomException(404, "Application is not found");

//        await repository.DeleteAsync(applicationtable, id);

//        return true;
//    }


//    public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
//    {
//        var Applications = (await repository.GetAllAsync(applicationtable))
//            .Where(a => !a.IsDeleted);

//        return mapper.Map<IEnumerable<ApplicationViewModel>>(Applications);
//    }


//    public async Task<ApplicationViewModel> GetByIdAsync(long id)
//    {
//        var existApplication = (await repository.GetByIdAsync(applicationtable, id))
//            ?? throw new CustomException(404, "Applicaion is not found");

//        return mapper.Map<ApplicationViewModel>(existApplication);
//    }


//    public async Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel application)
//    {
//        var existUser = await userService.GetByIdAsync(application.UserId);
//        var existVacancy = await jobVacancyService.GetByIdAsync(application.JobVacancyId);

//        var existApplication = (await repository.GetByIdAsync(applicationtable, id))
//            ?? throw new CustomException(404, "Applicaion is not found");

//        var mappedApplication = mapper.Map(application, existApplication);
//        var updatedApplication = repository.UpdateAsync(applicationtable, mappedApplication);

//        return mapper.Map<ApplicationViewModel>(updatedApplication);
//    }
// }
