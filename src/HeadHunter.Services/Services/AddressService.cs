using AutoMapper;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;

namespace HeadHunter.Services.Services;

public class AddressService : IAddressService
{
    private readonly IMapper mapper;
    private readonly IGenericRepository<Address> repository;

    public AddressService(IMapper mapper, IGenericRepository<Address> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<AddressViewModel> CreateAsync(AddressCreateModel address)
    {
        var existAddress = repository
        .SelectAsQueryable()
        .FirstOrDefault
        (u => u.Country.ToLower() == address.Country.ToLower() || u.City.ToLower() == address.City.ToLower());

        if (existAddress is not null)
            throw new CustomException(409,"Address already exist");

        var createdAddress = await repository.InsertAsync(mapper.Map<Address>(address));
        await repository.SaveAsync();

        return mapper.Map<AddressViewModel>(createdAddress);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAddress = await repository.SelectByIdAsync(id)
            ?? throw new CustomException(404,"Address not found");

        await repository.DeleteAsync(existAddress);
        await repository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<AddressViewModel>> GetAllAsync()
    {
        var addresses = await repository
            .SelectAsQueryable(
                new string[] {"Address" }).ToListAsync();

        return mapper.Map<IEnumerable<AddressViewModel>>(addresses);
    }

    public async Task<AddressViewModel> GetByIdAsync(long id)
    {
        var existAddress = await repository.SelectByIdAsync(id, new string[] {"Address", })
            ?? throw new CustomException(404, "Address not found");

        return mapper.Map<AddressViewModel>(existAddress);
    }

    public async Task<AddressViewModel> UpdateAsync(long id, AddressUpdateModel address)
    {
        var existAddress = await repository.SelectByIdAsync(id)
             ?? throw new CustomException(404, "Address not found");

        var mappedAddress = mapper.Map(address, existAddress);
        var updatedAddress = await repository.UpdateAsync(mappedAddress);
        await repository.SaveAsync();

        return mapper.Map<AddressViewModel>(updatedAddress);
    }
}
