using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.JobVacancies;
using HeadHunter.Services.Services.Users;

namespace HeadHunter.Services.Services.Applications;

public class ApplicationService : IApplicationService
{
    private IMapper mapper;
    private IRepository<Application> repository;
    private IUserService userService;
    private IJobVacancyService jobVacancyService;
    public readonly string applicationTable = Constants.ApplicationTableName;
    public readonly string jobVacancyTable = Constants.JobVacancyTableName;


    public ApplicationService(IMapper mapper, IUserService userService, IJobVacancyService jobVacancy, IRepository<Application> repository)
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

        var existApplication = (await repository.GetAllAsync(applicationTable))
            .FirstOrDefault(a => a.UserId == application.UserId && a.VacancyId == application.VacancyId);

        if (existApplication != null)
            throw new CustomException(409, "You already applied");
        if (existApplication.IsDeleted)
            throw new CustomException(410, "Application is already deleted");

        var mapped = mapper.Map<Domain.Entities.Core.Application>(application);
        mapped.Id = (await repository.GetAllAsync(jobVacancyTable)).Last().Id + 1;
        var created = await repository.InsertAsync(applicationTable, mapped);

        return new ApplicationViewModel
        {
            Id = created.Id,
            JobVacancy = existJobVacancy,
            User = existUser
        };
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existApplication = (await repository.GetByIdAsync(applicationTable, id))
            ?? throw new CustomException(404, "Application is not found");

        if (existApplication.IsDeleted)
            throw new CustomException(410, "Application is already deleted");

        await repository.DeleteAsync(applicationTable, id);

        return true;
    }


    public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
    {
        var applications = await repository.GetAllAsync(applicationTable);
        var applicationTasks = applications
            .Where(a => !a.IsDeleted)
            .Select(async app =>
            {
                var existUserTask = userService.GetByIdAsync(app.UserId);
                var existJobVacancyTask = jobVacancyService.GetByIdAsync(app.VacancyId);
                var mapped = mapper.Map<ApplicationViewModel>(app);

                mapped.Id = app.Id;

                var existUser = await existUserTask ?? new UserViewModel();
                var existJobVacancy = await existJobVacancyTask ?? new JobVacancyViewModel();

                mapped.User = existUser;
                mapped.JobVacancy = existJobVacancy;

                return mapped;
            });

        var mappedApplications = await Task.WhenAll(applicationTasks);
        return mappedApplications;
    }


    public async Task<ApplicationViewModel> GetByIdAsync(long id)
    {
        var existApplication = (await repository.GetByIdAsync(applicationTable, id))
            ?? throw new CustomException(404, "Application is not found");

        var existUser = await userService.GetByIdAsync(existApplication.UserId);
        var existJobVacancy = await jobVacancyService.GetByIdAsync(existApplication.VacancyId);

        return new ApplicationViewModel
        {
            Id = id,
            User = existUser,
            JobVacancy = existJobVacancy,
        };
    }


    public async Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel application)
    {
        var existUser = await userService.GetByIdAsync(application.UserId);
        var existVacancy = await jobVacancyService.GetByIdAsync(application.JobVacancyId);

        var existApplication = (await repository.GetByIdAsync(applicationTable, id))
            ?? throw new CustomException(404, "Application is not found");

        if (existApplication.IsDeleted)
            throw new CustomException(410, "Application is already deleted");

        var mappedApplication = mapper.Map(application, existApplication);
        var updatedApplication = repository.UpdateAsync(applicationTable, mappedApplication);

        return new ApplicationViewModel
        {
            Id = updatedApplication.Id,
            JobVacancy = existVacancy,
            User = existUser
        };
    }
}
