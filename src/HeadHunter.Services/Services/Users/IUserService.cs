using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.Services.Users;
public interface IUserService
{
    Task<UserViewModel> CreateAsync(UserCreateModel user);
    Task<UserViewModel> UpdateAsync(long id,UserUpdateModel user);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync();
}
