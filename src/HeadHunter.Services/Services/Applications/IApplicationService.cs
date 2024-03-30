using HeadHunter.Services.DTOs.Core.Dtos.Appication.Dtos;

namespace HeadHunter.Services.Services.Applications;

public interface IApplicationService
{
    Task<ApplicationViewModel> CreateAsync(ApplicationUpdateModel address);
    Task<ApplicationViewModel> UpdateAsync(long id, ApplicationUpdateModel address);
    Task<bool> DeleteAsync(long id);
    Task<ApplicationViewModel> GetByIdAsync(long id);
    Task<IEnumerable<ApplicationViewModel>> GetAllAsync();
}
