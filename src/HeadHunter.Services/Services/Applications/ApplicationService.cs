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
    private readonly IMapper _mapper;
    private readonly IRepository<Application> _repository;
    private readonly string _applicationTable;

    public ApplicationService(IMapper mapper, IRepository<Application> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _applicationTable = Constants.ApplicationTableName;
    }

    public async Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application)
    {
        var existApplication = (await _repository.GetAllAsync(_applicationTable))
            .FirstOrDefault(a => a.UserId == application.UserId && a.VacancyId == application.VacancyId);

        if (existApplication != null)
            throw new CustomException(409, "You already applied");

        var mapped = _mapper.Map<Domain.Entities.Core.Application>(application);
        mapped.Id = (await _repository.GetAllAsync(_applicationTable)).LastOrDefault()?.Id + 1 ?? 1;
        var created = await _repository.InsertAsync(_applicationTable, mapped);

        return new ApplicationViewModel
        {
            Id = created.Id,
            UserId = created.UserId,
            JobVacancyId = created.VacancyId
        };
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existApplication = await _repository.GetByIdAsync(_applicationTable, id)
            ?? throw new CustomException(404, "Application is not found");

        if (existApplication.IsDeleted)
            throw new CustomException(410, "Application is already deleted");

        await _repository.DeleteAsync(_applicationTable, id);

        return true;
    }

    public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
    {
        var applications = await _repository.GetAllAsync(_applicationTable);
        var mappedApplications = _mapper.Map<IEnumerable<ApplicationViewModel>>(applications.Where(a => !a.IsDeleted));
        return mappedApplications;
    }

    public async Task<ApplicationViewModel> GetByIdAsync(long id)
    {
        var existApplication = await _repository.GetByIdAsync(_applicationTable, id)
            ?? throw new CustomException(404, "Application is not found");

        return new ApplicationViewModel
        {
            Id = existApplication.Id,
            UserId = existApplication.UserId,
            JobVacancyId = existApplication.VacancyId
        };
    }

    public async Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel application)
    {
        var existApplication = await _repository.GetByIdAsync(_applicationTable, id)
            ?? throw new CustomException(404, "Application is not found");

        if (existApplication.IsDeleted)
            throw new CustomException(410, "Application is already deleted");

        var mappedApplication = _mapper.Map(application, existApplication);
        var updatedApplication = await _repository.UpdateAsync(_applicationTable, mappedApplication);

        return new ApplicationViewModel
        {
            Id = updatedApplication.Id,
            UserId = updatedApplication.UserId,
            JobVacancyId = updatedApplication.VacancyId
        };
    }
}