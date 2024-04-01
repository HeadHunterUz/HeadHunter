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
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;
    private readonly string _userTable = Constants.UserTableName;

    public UserService(IMapper mapper, IRepository<User> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UserViewModel> CreateAsync(UserCreateModel user)
    {
        var existUser = (await _repository.GetAllAsync(_userTable))
            .FirstOrDefault(a => a.IndustryId == user.IndustryId && a.AddressId == user.AddressId);

        if (existUser != null)
            throw new CustomException(409, "User already exists");

        var created = _mapper.Map<User>(user);
        created.Id = await GenerateNewId();
        await _repository.InsertAsync(_userTable, created);

        return _mapper.Map<UserViewModel>(created);
    }

    private async Task<long> GenerateNewId()
    {
        var users = await _repository.GetAllAsync(_userTable);
        var maxId = users.Any() ? users.Max(u => u.Id) : 0;
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await _repository.GetByIdAsync(_userTable, id)
            ?? throw new CustomException(404, "User not found");

        await _repository.DeleteAsync(_userTable, id);

        return true;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        var users = (await _repository.GetAllAsync(_userTable))
            .Where(a => !a.IsDeleted);

        return _mapper.Map<IEnumerable<UserViewModel>>(users);
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await _repository.GetByIdAsync(_userTable, id)
            ?? throw new CustomException(404, "User not found");

        return _mapper.Map<UserViewModel>(existUser);
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
    {
        var existUser = await _repository.GetByIdAsync(_userTable, id)
            ?? throw new CustomException(404, "User not found");

        var mappedUser = _mapper.Map(user, existUser);

        return _mapper.Map<UserViewModel>(mappedUser);
    }
}
