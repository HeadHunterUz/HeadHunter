using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Services.DTOs.Admins.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Admins;

public class AdminService : IAdminService
{
    private readonly IRepository<Admin> _repository;
    private readonly IMapper _mapper;
    private readonly string _adminTableName;

    public AdminService(IMapper mapper, IRepository<Admin> repository, string adminTableName = Constants.AdminTableName)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _adminTableName = adminTableName ?? throw new ArgumentNullException(nameof(adminTableName));
    }

    public async Task<AdminViewModel> CreateAsync(AdminCreateModel admin)
    {
        var existAdminWithPhoneNumber = (await _repository.GetAllAsync(_adminTableName))
            .FirstOrDefault(a => a.Phone == admin.Phone);
        var existAdminWithEmail = (await _repository.GetAllAsync(_adminTableName))
            .FirstOrDefault(a => a.Email == admin.Email);

        if (existAdminWithEmail != null)
        {
            throw new CustomException(409, "Admin with this email already exists");
        }

        if (existAdminWithPhoneNumber != null)
        {
            throw new CustomException(409, "Admin with this phone number already exists");
        }

        var createdAdmin = _mapper.Map<Admin>(admin);
        createdAdmin.Id = (await _repository.GetAllAsync(_adminTableName)).LastOrDefault()?.Id + 1 ?? 1;
        await _repository.InsertAsync(_adminTableName, createdAdmin);

        return _mapper.Map<AdminViewModel>(createdAdmin);
    }

    public async Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel admin)
    {
        var existAdmin = await _repository.GetByIdAsync(_adminTableName, id)
            ?? throw new Exception("Admin is not found");

        var mapped = _mapper.Map(admin, existAdmin);
        var updated = await _repository.UpdateAsync(_adminTableName, mapped);

        return _mapper.Map<AdminViewModel>(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAdmin = await _repository.GetByIdAsync(_adminTableName, id)
            ?? throw new CustomException(404, "Admin is not found");

        if (existAdmin.IsDeleted)
        {
            throw new CustomException(410, "Admin is already deleted");
        }

        await _repository.DeleteAsync(_adminTableName, id);
        return true;
    }

    public async Task<IEnumerable<AdminViewModel>> GetAllAsync()
    {
        var admins = await _repository.GetAllAsync(_adminTableName);
        var mappedAdmins = _mapper.Map<IEnumerable<AdminViewModel>>(admins.Where(a => !a.IsDeleted));
        return mappedAdmins;
    }

    public async Task<AdminViewModel> GetByIdAsync(long id)
    {
        var existAdmin = await _repository.GetByIdAsync(_adminTableName, id)
            ?? throw new CustomException(404, "Admin is not found");

        if (existAdmin.IsDeleted)
        {
            throw new CustomException(410, "Admin is already deleted");
        }

        return _mapper.Map<AdminViewModel>(existAdmin);
    }
}