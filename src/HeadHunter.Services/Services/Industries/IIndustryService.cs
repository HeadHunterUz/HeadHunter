using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Services.Services.Industries;

public interface IIndustryService
{
    Task<IndustryViewModel> CreateAsync(IndustryCreateModel model);
    Task<IndustryViewModel> UpdateAsync(long id, IndustryUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<IndustryViewModel> GetByIdAsync(long id);
    IEnumerable<IndustryViewModel> GetAllAsEnumerableAsync();
    IQueryable<IndustryViewModel> GetAllAsQueryable();
}
