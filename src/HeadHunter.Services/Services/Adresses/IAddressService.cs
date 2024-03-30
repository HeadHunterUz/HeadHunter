using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.Services.Adresses;
public interface IAddressService
{
    Task<AddressViewModel> CreateAsync(AddressCreateModel address);
    Task<AddressViewModel> UpdateAsync(long id, AddressUpdateModel address);
    Task<bool> DeleteAsync(long id);
    Task<AddressViewModel> GetByIdAsync(long id);
    Task<IEnumerable<AddressViewModel>> GetAllAsync();
}
