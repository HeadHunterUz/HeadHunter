using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.JobVacancies;

namespace HeadHunter.Services.Services.Applications;

public class ApplicationService : IApplicationService
{
    private IMapper mapper;
    private IRepository<Application> repository;
    private IUserService userService;
    private IJobVacancy jobVacancyService;
    public readonly string table = Constants.ApplicationTableName;

    public ApplicationService(IMapper mapper,IUserService userService,IJobVacancy jobVacancy, IRepository<Application> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
        this.jobVacancyService = jobVacancy;
    }

    public async Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application)
    {
        var existUser = await userService.GetByIdAsync(application.UserId);
        var existJobVacancy = await jobVacancyService.GetByIdAsync(application.VacancyId);

        var existApplication = (await repository.GetAllAsync(table))
            .FirstOrDefault(a => a.UserId == application.UserId && a.VacancyId == application.VacancyId);

        if (existApplication != null)
            throw new CustomException(409, "You already applied");

        var completed = mapper.Map<Application>(application);
        await repository.InsertAsync(table, completed);

        var viewModel = mapper.Map<ApplicationViewModel>(completed);

        viewModel.JobVacancy = mapper.Map<JobVacancyViewModel>(existJobVacancy);
        viewModel.User = mapper.Map<UserViewModel>(existUser);

        return viewModel;
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel application)
    {
        throw new NotImplementedException();
    }
}
