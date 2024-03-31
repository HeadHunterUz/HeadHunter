using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;

namespace HeadHunter.Services.Services.Applications;

public interface IApplicationService
{
    Task<ApplicationViewModel> CreateAsync(ApplicationCreateModel application);
    Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel application);
    Task<bool> DeleteAsync(long id);
    Task<ApplicationViewModel> GetByIdAsync(long id);
    Task<IEnumerable<ApplicationViewModel>> GetAllAsync();
}
