using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Users;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.Industries;

namespace HeadHunter.Services.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> repository;
    private readonly IndustryService industryService;
    private readonly IAddressService addressService;
    public readonly string userTable = Constants.UserTableName;
    public readonly string industrytable = Constants.IndustryTableName;
    public readonly string addresstable = Constants.AddressTableName;

    public UserService(IMapper mapper, IndustryService industryService, IAddressService addressService, IRepository<User> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.industryService = industryService;
        this.addressService = addressService;
    }

    public async Task<UserViewModel> CreateAsync(UserCreateModel user)
    {
        var existIndustry = await industryService.GetByIdAsync(user.IndustryId);
        var existAddress = await addressService.GetByIdAsync(user.AddressId);

        var existUser = (await repository.GetAllAsync(userTable))
            .FirstOrDefault(a => a.IndustryId == user.IndustryId && a.AddressId == user.AddressId);

        if (existUser != null)
            throw new CustomException(409, "User is existed");

        var completed = mapper.Map<User>(user);
        completed.Id = await GenerateNewId();
        await repository.InsertAsync(userTable, completed);

        var viewModel = mapper.Map<UserViewModel>(completed);

        viewModel.Address = mapper.Map<AddressViewModel>(existAddress);
        viewModel.Industry = mapper.Map<IndustryViewModel>(existIndustry);

        return viewModel;

    }
    private async Task<long> GenerateNewId()
    {
        long maxId = (await repository.GetAllAsync(userTable)).Max(v => v.Id);
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = (await repository.GetByIdAsync(userTable, id))
            ?? throw new CustomException(404, "User not found");

        await repository.DeleteAsync(userTable, id);

        return true;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        var Users = (await repository.GetAllAsync(userTable))
             .Where(a => !a.IsDeleted);

        return mapper.Map<IEnumerable<UserViewModel>>(Users);
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = (await repository.GetByIdAsync(userTable, id))
          ?? throw new CustomException(404, "User not found");

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
    {
        var existIndustry = await industryService.GetByIdAsync(user.IndustryId);
        var existAddress = await addressService.GetByIdAsync(user.AddressId);

        var existCompany = (await repository.GetByIdAsync(userTable, id))
            ?? throw new CustomException(404, "User is not found");
        var mapped = mapper.Map<UserViewModel>(existCompany);

        mapped.Industry = existIndustry;
        mapped.Address = existAddress;

        return mapped;
    }
}
