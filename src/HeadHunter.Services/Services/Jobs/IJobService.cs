using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Services.Services.Jobs;

public interface IJobService
{
    Task<JobViewModel> CreateAsync(JobCreateModel model);
    Task<JobViewModel> UpdateAsync(long id, JobUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<JobViewModel> GetByIdAsync(long id);
    IEnumerable<JobViewModel> GetAllAsEnumerable();
    IQueryable<JobViewModel> GetAllAsQueyable();
}
