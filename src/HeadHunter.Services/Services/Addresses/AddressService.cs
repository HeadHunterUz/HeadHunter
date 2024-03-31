using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.Exceptions;

namespace HeadHunter.Services.Services.Addresses;

public class AddressService : IAddressService
{
    private readonly IMapper mapper;
    private readonly IRepository<Address> repository;
    public readonly string table = Constants.AddressTableName;

    public AddressService(IMapper mapper, IRepository<Address> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<AddressViewModel> CreateAsync(AddressCreateModel address)
    {
        var existAddress = (await repository.GetAllAsync(table))
            .FirstOrDefault(u => u.Country.ToLower() == address.Country.ToLower() || u.City.ToLower() == address.City.ToLower());

        if (existAddress != null)
            throw new CustomException(409, "Address already exists");

        var createdAddress = mapper.Map<Address>(address);
        await repository.InsertAsync(table, createdAddress);

        return mapper.Map<AddressViewModel>(createdAddress);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAddress = await repository.GetByIdAsync(table, id)
            ?? throw new CustomException(404, "Address not found");

        await repository.DeleteAsync(table, id);

        return true;
    }

    public async Task<IEnumerable<AddressViewModel>> GetAllAsync()
    {
        var addresses = await repository.GetAllAsync(table);

        return mapper.Map<IEnumerable<AddressViewModel>>(addresses);
    }

    public async Task<AddressViewModel> GetByIdAsync(long id)
    {
        var existAddress = await repository.GetByIdAsync(table, id)
            ?? throw new CustomException(404, "Address not found");

        return mapper.Map<AddressViewModel>(existAddress);
    }

    public async Task<AddressViewModel> UpdateAsync(long id, AddressUpdateModel address)
    {
        var existAddress = await repository.GetByIdAsync(table, id)
             ?? throw new CustomException(404, "Address not found");

        var mappedAddress = mapper.Map(address, existAddress);

        await repository.UpdateAsync(table, mappedAddress);

        return mapper.Map<AddressViewModel>(mappedAddress);
    }
}
