using AutoMapper;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Appication.Dtos;

namespace HeadHunter.Services.Services.Applications;

public class ApplicationService : IApplicationService
{
    private IMapper mapper;
    private IRepository<Application> repository;

    public async Task<ApplicationViewModel> CreateAsync(ApplicationUpdateModel address)
    {
        throw new NotImplementedException();
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

    public Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel address)
    {
        throw new NotImplementedException();
    }
}
