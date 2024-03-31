using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Services.DTOs.Admins.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;

namespace HeadHunter.Services.Services.Admins;

public class AdminService : IAdminService
{
    private IRepository<Admin> repository;
    private IAddressService addressService;
    private IMapper mapper;

    private readonly string admintable = Constants.AdminTableName;
    public AdminService(IMapper mapper, IAddressService address, IRepository<Admin> repository)
    {
        this.repository = repository;
        this.addressService = address;
        this.mapper = mapper;
    }
    public async Task<AdminViewModel> CreateAsync(AdminCreateModel admin)
    {
        var existAddress = await addressService.GetByIdAsync(admin.AddressId);

        var existAdminWithPhoneNumber = (await repository.GetAllAsync(admintable))
            .Where(a => a.Phone == admin.Phone);
        var existAdminWithEmail = (await repository.GetAllAsync(admintable))
            .Where(a => a.Email == admin.Email);

        if (existAdminWithEmail != null)
            throw new CustomException(409, "Admin with this email is already exists");
        if (existAdminWithPhoneNumber != null)
            throw new CustomException(409, "Admin with this phone number is already exists");

        var created = await repository.InsertAsync(admintable, mapper.Map<Admin>(admin));

        return new AdminViewModel
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            Email = created.Email,
            Phone = created.Phone,
            Address = existAddress
        };
    }
    public async Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel admin)
    {
        var existAddress = await addressService.GetByIdAsync(admin.AddressId);

        var existAdmin = await repository.GetByIdAsync(admintable, id)
            ?? throw new Exception("Admin is not found");

        var mapped = mapper.Map(admin, existAdmin);
        var updated = await repository.UpdateAsync(admintable, mapped);

        return new AdminViewModel
        {
            Id = updated.Id,
            FirstName = updated.FirstName,
            LastName = updated.LastName,
            Email = updated.Email,
            Phone = updated.Phone,
            Address = existAddress
        };
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var existAdmin = await repository.GetByIdAsync(admintable, id)
            ?? throw new CustomException(404, "Admin is not found");

        if (existAdmin.IsDeleted)
            throw new CustomException(410, "Admin is already deleted");

        await repository.DeleteAsync(admintable, id);
        return true;
    }
    public async Task<IEnumerable<AdminViewModel>> GetAllAsync()
    {
        var Admins = (await repository.GetAllAsync(admintable))
            .Where(a => !a.IsDeleted);

        return mapper.Map<IEnumerable<AdminViewModel>>(Admins);
    }
    public async Task<AdminViewModel> GetByIdAsync(long id)
    {
        var existAdmin = await repository.GetByIdAsync(admintable, id)
            ?? throw new CustomException(404, "Admin is not found");
        if (existAdmin.IsDeleted)
            throw new CustomException(410, "Admin is already deleted");

        return mapper.Map<AdminViewModel>(existAdmin);
    }
}
