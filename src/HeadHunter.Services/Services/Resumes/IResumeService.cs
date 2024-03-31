using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;

namespace HeadHunter.Services.Services.Resumes;
public interface IResumeService
{
    Task<ResumeViewModel> CreateAsync(ResumeCreateModel resume);
    Task<ResumeViewModel> UpdateAsync(long id, ResumeUpdateModel resume);
    Task<bool> DeleteAsync(long id);
    Task<ResumeViewModel> GetByIdAsync(long id);
    Task<IEnumerable<ResumeViewModel>> GetAllAsync();
}
