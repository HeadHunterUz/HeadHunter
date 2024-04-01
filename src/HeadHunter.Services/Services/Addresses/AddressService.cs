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
        var addresses = await repository.GetAllAsync(table);
        var existAddress = addresses
            .FirstOrDefault(u => u.Country.ToLower() == address.Country.ToLower() && u.City.ToLower() == address.City.ToLower() && !u.IsDeleted);

        if (existAddress != null)
            throw new CustomException(409, "Address already exists");

        var mapped = mapper.Map<Address>(address);

        mapped.Id = addresses.Count()==0 ? 1 : addresses.Last().Id + 1;
        var created = repository.InsertAsync(table, mapped);
        
        return mapper.Map<AddressViewModel>(created.Result);
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
        var addresses = (await repository.GetAllAsync(table)).Where(a => !a.IsDeleted);

        return mapper.Map<IEnumerable<AddressViewModel>>(addresses);
    }

    public async Task<AddressViewModel> GetByIdAsync(long id)
    {
        var existAddress = await repository.GetByIdAsync(table, id)
            ?? throw new CustomException(404, "Address not found");
        if (existAddress.IsDeleted)
            throw new CustomException(410, "Address is already deleted");

        return mapper.Map<AddressViewModel>(existAddress);
    }

    public async Task<AddressViewModel> UpdateAsync(long id, AddressUpdateModel address)
    {
        var existAddress = await repository.GetByIdAsync(table, id)
             ?? throw new CustomException(404, "Address not found");
        if (existAddress.IsDeleted)
            throw new CustomException(410, "Address is already deleted");

        var mappedAddress = mapper.Map(address, existAddress);
        var updatedAddress = await repository.UpdateAsync(table, mappedAddress);

        return mapper.Map<AddressViewModel>(updatedAddress);
    }
}
