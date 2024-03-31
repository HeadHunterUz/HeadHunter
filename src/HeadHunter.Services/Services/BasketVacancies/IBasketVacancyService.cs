using HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;

namespace HeadHunter.Services.Services.BasketVacancies;

public interface IBasketVacancyService
{
    Task<BasketVacancyViewModel> CreateAsync(BasketVacancyCreateModel basketVacancy);
    Task<BasketVacancyViewModel> UpdateAsync(long id, BasketVacancyUpdateModel basketVacancy);
    Task<bool> DeleteAsync(long id);
    Task<BasketVacancyViewModel> GetByIdAsync(long id);
    Task<IEnumerable<BasketVacancyViewModel>> GetAllAsync();
}
