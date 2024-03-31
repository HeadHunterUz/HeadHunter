using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
using HeadHunter.Services.Exceptions;

namespace HeadHunter.Services.Services.Applications;

public class ApplicationService : IApplicationService
{
    private IMapper mapper;
    private IRepository<Application> repository;
    public readonly string table = Constants.ApplicationTableName;

    public ApplicationService(IMapper mapper, IRepository<Application> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application)
    {
        var existApplication = (await repository.GetAllAsync(table))
            .FirstOrDefault(a => a.UserId == application.UserId && a.VacancyId == application.VacancyId);
        if (existApplication != null)
            throw new CustomException(409, "You already applied");
        var completed = mapper.Map<Application>(application);
        await repository.InsertAsync(table, completed);

        return mapper.Map<ApplicationViewModel>(application);
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
