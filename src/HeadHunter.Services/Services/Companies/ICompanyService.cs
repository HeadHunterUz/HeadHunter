using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

namespace HeadHunter.Services.Services.Companies;
public interface ICompanyService
{
    Task<CompanyViewModel> CreateAsync(CompanyCreateModel company);
    Task<CompanyViewModel> UpdateAsync(long id, CompanyUpdateModel company);
    Task<bool> DeleteAsync(long id);
    Task<CompanyViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CompanyViewModel>> GetAllAsync();
}

