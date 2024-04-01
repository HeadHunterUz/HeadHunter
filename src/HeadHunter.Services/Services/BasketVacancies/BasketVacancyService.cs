using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Vacancies;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.JobVacancies;
using HeadHunter.Services.Services.Users;

namespace HeadHunter.Services.Services.BasketVacancies;

public class BasketVacancyService : IBasketVacancyService
{
    private readonly IMapper _mapper;
    private readonly IRepository<BasketVacancy> _repository;
    private readonly string _basketVacancyTable;

    public BasketVacancyService(IMapper mapper, IRepository<BasketVacancy> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _basketVacancyTable = Constants.BasketVacancyTableName;
    }

    public async Task<BasketVacancyViewModel> CreateAsync(BasketVacancyCreateModel basketVacancy)
    {
        var existBasketVacancy = (await _repository.GetAllAsync(_basketVacancyTable))
            .FirstOrDefault(b => b.JobVacancyId == basketVacancy.JobVacancyId && b.UserId == basketVacancy.UserId);

        if (existBasketVacancy != null)
            throw new CustomException(409, "BasketVacancy already exists");

        var mapped = _mapper.Map<BasketVacancy>(basketVacancy);
        mapped.Id = (await _repository.GetAllAsync(_basketVacancyTable)).LastOrDefault()?.Id + 1 ?? 1;
        var created = await _repository.InsertAsync(_basketVacancyTable, mapped);

        return new BasketVacancyViewModel
        {
            Id = created.Id,
            JobVacancyId = created.JobVacancyId,
            UserId = created.UserId
        };
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existBasketVacancy = await _repository.GetByIdAsync(_basketVacancyTable, id)
            ?? throw new CustomException(404, "BasketVacancy not found");

        if (existBasketVacancy.IsDeleted)
            throw new CustomException(410, "BasketVacancy already deleted");

        await _repository.DeleteAsync(_basketVacancyTable, id);
        return true;
    }

    public async Task<IEnumerable<BasketVacancyViewModel>> GetAllAsync()
    {
        var basketVacancies = await _repository.GetAllAsync(_basketVacancyTable);
        var mappedBasketVacancies = _mapper.Map<IEnumerable<BasketVacancyViewModel>>(basketVacancies.Where(b => !b.IsDeleted));
        return mappedBasketVacancies;
    }

    public async Task<BasketVacancyViewModel> GetByIdAsync(long id)
    {
        var existBasketVacancy = await _repository.GetByIdAsync(_basketVacancyTable, id)
            ?? throw new CustomException(404, "BasketVacancy not found");

        return new BasketVacancyViewModel
        {
            Id = existBasketVacancy.Id,
            JobVacancyId = existBasketVacancy.JobVacancyId,
            UserId = existBasketVacancy.UserId
        };
    }

    public async Task<BasketVacancyViewModel> UpdateAsync(long id, BasketVacancyUpdateModel basketVacancy)
    {
        var existBasketVacancy = await _repository.GetByIdAsync(_basketVacancyTable, id)
            ?? throw new CustomException(404, "BasketVacancy not found");

        if (existBasketVacancy.IsDeleted)
            throw new CustomException(410, "BasketVacancy already deleted");

        var mappedBasketVacancy = _mapper.Map(basketVacancy, existBasketVacancy);
        var updatedBasketVacancy = await _repository.UpdateAsync(_basketVacancyTable, mappedBasketVacancy);

        return new BasketVacancyViewModel
        {
            Id = updatedBasketVacancy.Id,
            JobVacancyId = updatedBasketVacancy.JobVacancyId,
            UserId = updatedBasketVacancy.UserId
        };
    }
}