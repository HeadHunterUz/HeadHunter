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
    private IMapper mapper;
    private IRepository<BasketVacancy> repository;
    private IUserService userService;
    private IJobVacancyService jobVacancyService;
    private readonly string basketvacancytable = Constants.BasketVacancyTableName;


    public BasketVacancyService(IMapper mapper, IRepository<BasketVacancy> repository, IUserService userService, IJobVacancyService jobVacancyService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
        this.jobVacancyService = jobVacancyService;
    }


    public async Task<BasketVacancyViewModel> CreateAsync(BasketVacancyCreateModel basketVacancy)
    {
        var existsUser = await userService.GetByIdAsync(basketVacancy.UserId);
        var existJobVacancy = await jobVacancyService.GetByIdAsync(basketVacancy.JobVacancyId);

        var existBasketVacancy = (await repository.GetAllAsync(basketvacancytable))
           .FirstOrDefault(b => b.VacancyId == basketVacancy.JobVacancyId && b.UserId == existsUser.Id);

        if (existBasketVacancy != null)
            throw new CustomException(409, "BasketVacancy is already exists");

        var created = await repository.InsertAsync(basketvacancytable, mapper.Map<BasketVacancy>(basketVacancy));

        return new BasketVacancyViewModel
        {
            Id = created.Id,
            User = existsUser,
            JobVacancy = existJobVacancy,
        };
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var existBasketVacancy = await repository.GetByIdAsync(basketvacancytable, id)
            ?? throw new CustomException(404, "BasketVacancy is not found");

        if (existBasketVacancy.IsDeleted)
            throw new CustomException(410, "BasketVacancy is already deleted");

        await repository.DeleteAsync(basketvacancytable, id);
        return true;
    }


    public async Task<IEnumerable<BasketVacancyViewModel>> GetAllAsync()
    {
        var basketVacancies = await repository.GetAllAsync(basketvacancytable);
        var basketVacanciesTasks = basketVacancies
            .Where(a => !a.IsDeleted)
            .Select(async bv =>
            {
                var existUserTask = userService.GetByIdAsync(bv.UserId);
                var existJobVacancyTask = jobVacancyService.GetByIdAsync(bv.VacancyId);
                var mapped = mapper.Map<BasketVacancyViewModel>(bv);

                mapped.Id = bv.Id;

                var existUser = await existUserTask ?? new UserViewModel();
                var existJobVacancy = await existJobVacancyTask ?? new JobVacancyViewModel();

                mapped.User = existUser;
                mapped.JobVacancy = existJobVacancy;

                return mapped;
            });

        var mappedBasketVacancies = await Task.WhenAll(basketVacanciesTasks);
        return mappedBasketVacancies;
    }


    public async Task<BasketVacancyViewModel> GetByIdAsync(long id)
    {

        var existsVacancy = await repository.GetByIdAsync(basketvacancytable, id)
           ?? throw new CustomException(404, "BasketVacancy is not found");
        if (existsVacancy.IsDeleted)
            throw new CustomException(410, "BasketVacancy is already deleted");

        var existsUser = await userService.GetByIdAsync(existsVacancy.UserId);
        var existJobVacancy = await jobVacancyService.GetByIdAsync(existsVacancy.VacancyId);

        return new BasketVacancyViewModel
        {
            Id = id,
            User = existsUser,
            JobVacancy = existJobVacancy
        };
    }


    public async Task<BasketVacancyViewModel> UpdateAsync(long id, BasketVacancyUpdateModel basketVacancy)
    {
        var existsUser = await userService.GetByIdAsync(basketVacancy.UserId);
        var existJobVacancy = await jobVacancyService.GetByIdAsync(basketVacancy.JobVacancyId);

        var existsBasketVacancy = await repository.GetByIdAsync(basketvacancytable, id)
           ?? throw new CustomException(404, "BasketVacancy is not found");

        var mapped = mapper.Map(basketVacancy, existsBasketVacancy);
        await repository.UpdateAsync(basketvacancytable, mapped);

        return mapper.Map<BasketVacancyViewModel>(mapped);
    }
}