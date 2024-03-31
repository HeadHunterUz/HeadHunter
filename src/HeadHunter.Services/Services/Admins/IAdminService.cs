using HeadHunter.Services.DTOs.Admins.Dtos;

namespace HeadHunter.Services.Services.Admins;

public interface IAdminService
{
    Task<AdminViewModel> CreateAsync(AdminCreateModel model);
    Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<AdminViewModel>> GetAllAsync();
    Task<AdminViewModel> GetByIdAsync(long id);
}
