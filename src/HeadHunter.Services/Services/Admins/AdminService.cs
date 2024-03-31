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
    public async Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel model)
    {
        var existAdmin = await repository.GetByIdAsync(admintable, id)
            ?? throw new Exception("Admin is not found");
        var 
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var exist = admins.FirstOrDefault(a => a.Id == id);
        if (exist is null)
            throw new Exception("This admin is not found");

        exist.IsDeleted = true;
        admins.Remove(exist);
        await repository.DeleteAsync(table, id);
        return true;
    }
    public IEnumerable<AdminViewModel> GetAllAsEnumerable()
    {
        return mapper.Map<IEnumerable<AdminViewModel>>(admins);
    }
    public IQueryable<AdminViewModel> GetAllAsQueryable()
    {
        return mapper.Map<IQueryable<AdminViewModel>>(admins);
    }
}
