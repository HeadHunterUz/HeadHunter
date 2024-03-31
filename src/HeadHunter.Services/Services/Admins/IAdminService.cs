using HeadHunter.DataAccess.IRepositories;
using HeadHunter.DataAccess.Repositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Services.DTOs.Admins.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Services.Services.Admins;

public interface IAdminService
{
    Task<AdminViewModel> CreateAsync(AdminCreateModel model);
    Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel model);
    Task<bool> DeleteAsync(long id);
    IEnumerable<AdminViewModel> GetAllAsEnumerable();
    IQueryable<AdminViewModel> GetAllAsQueryable();
}
