using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
namespace HeadHunter.Services.Services.Addresses;
public interface IAddressService
{
    Task<AddressViewModel> CreateAsync(AddressCreateModel address);
    Task<AddressViewModel> UpdateAsync(long id, AddressUpdateModel address);
    Task<bool> DeleteAsync(long id);
    Task<AddressViewModel> GetByIdAsync(long id);
    Task<IEnumerable<AddressViewModel>> GetAllAsync();
}
