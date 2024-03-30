using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

namespace HeadHunter.Services.Services.Companies;
public interface ICompanyService
{
    Task<CompaniesViewModel> CreateAsync(CompaniesCreateModel company);
    Task<CompaniesViewModel> UpdateAsync(long id, CompaniesUpdateModel company);
    Task<bool> DeleteAsync(long id);
    Task<CompaniesViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CompaniesViewModel>> GetAllAsync();
}

