using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Vacancies;
using HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.JobVacancies;

namespace HeadHunter.Services.Services.BasketVacancies;

public class BasketVacancyService : IBasketVacancyService
{
    private IMapper mapper;
    private IRepository<BasketVacancy> repository;
    private IUserService userService;
    private IJobVacancyService jobVacancyService;
    private readonly string basketvacancytable = Constants.BasketVacancyTableName;
    public BasketVacancyService(IMapper mapper, IRepository<BasketVacancy> repository,IUserService userService, IJobVacancyService jobVacancyService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
        this.userService = jobVacancyService;
    }

    public async Task<BasketVacancyViewModel> CreateAsync(BasketVacancyCreateModel basketVacancy)
    {
        var existsUser = await userService.GetByIdAsync(basketVacancy.UserId);
        var existJobVacancy = await jobVacancyService.GetByIdAsync(basketVacancy.JobVacancyId);

        var existBasketVacancy = (await repository.GetAllAsync(basketvacancytable))
            .Where(b => b.VacancyId == basketVacancy.JobVacancyId && b.UserId == existsUser.UserId);

        if (existBasketVacancy != null)
            throw new CustomException(409, "BasketVacancy is already exists");

        var created = await repository.InsertAsync(basketvacancytable, mapper.Map<BasketVacancy>(basketVacancy));

        var mapped = mapper.Map<BasketVacancyViewModel>(created);

        mapped.Id = created.Id;
        mapped.JobVacancy = existJobVacancy;
        mapped.User = existsUser;

        return mapped;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existBasketVacancy = await repository.GetByIdAsync(basketvacancytable, id)
             ?? throw new CustomException(404, "BasketVacancy is not found");

        await repository.DeleteAsync(basketvacancytable, id);
        return true;
    }

    public async Task<IEnumerable<BasketVacancyViewModel>> GetAllAsync()
    {
        var BasketVacancies = (await repository.GetAllAsync(basketvacancytable))
            .Where(b => !b.IsDeleted);

        return mapper.Map<IEnumerable<BasketVacancyViewModel>>(BasketVacancies);

    }

    public async Task<BasketVacancyViewModel> GetByIdAsync(long id)
    {
        var existsVacancy = await repository.GetByIdAsync(basketvacancytable, id)
            ?? throw new CustomException(404, "BasketVacancy is not found");

        return mapper.Map<BasketVacancyViewModel>(existsVacancy);
    }

    public async Task<BasketVacancyViewModel> UpdateAsync(long id, BasketVacancyUpdateModel basketVacancy)
    {
        var existsUser = await userService.GetByIdAsync(basketVacancy.UserId);
        var existJobVacancy = await jobVacancyService.GetByIdAsync(basketVacancy.JobVacancyId);

        var existsBasketVacancy = await repository.GetByIdAsync(basketvacancytable, id)
            ?? throw new CustomException(404, "BasketVacancy is not found");

        var mapped = mapper.Map(basketVacancy, existsBasketVacancy);
        await repository.UpdateAsync(basketvacancytable, mapped);

        return new BasketVacancyViewModel
        {
            Id = id,
            JobVacancy = existJobVacancy,
            User = existsUser,
        };
    }
}
