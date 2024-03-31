using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Companies;
using HeadHunter.Services.Services.Users;

namespace HeadHunter.Services.Services.Experiences;

public class ExperienceService : IExperienceService
{
    private IMapper mapper;
    private IRepository<Experience> repository;
    private IUserService userService;
    private ICompanyService companyService;
    public readonly string experiencetable = Constants.ExperienceTableName;
    public readonly string usertable = Constants.UserTableName;
    public readonly string companytable = Constants.CompanyTableName;

    public ExperienceService(IMapper mapper, IUserService userService, ICompanyService companyservice, IRepository<Experience> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
        this.companyService = companyService;
    }

    public async Task<ExperienceViewModel> CreateAsync(ExperienceCreateModel experience)
    {
        var existUser = await userService.GetByIdAsync(experience.UserId);
        var existCompany = await companyService.GetByIdAsync(experience.CompanyId);

        var existExperience = (await repository.GetAllAsync(experiencetable))
            .FirstOrDefault(a => a.UserId == experience.UserId && a.CompanyId == experience.CompanyId);

        if (existExperience != null)
            throw new CustomException(409, "Experience is existed");

        var completed = mapper.Map<Experience>(experience);
        await repository.InsertAsync(experiencetable, completed);

        var viewModel = mapper.Map<ExperienceViewModel>(completed);

        viewModel.Company = mapper.Map<CompanyViewModel>(existCompany);
        viewModel.User = mapper.Map<UserViewModel>(existUser);

        return viewModel;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCompany = (await repository.GetByIdAsync(companytable, id))
           ?? throw new CustomException(404, "No experience");

        await repository.DeleteAsync(experiencetable, id);

        return true;
    }

    public async Task<IEnumerable<ExperienceViewModel>> GetAllAsync()
    {
        var Experiences = (await repository.GetAllAsync(experiencetable))
           .Where(a => !a.IsDeleted);

        return mapper.Map<IEnumerable<ExperienceViewModel>>(Experiences);
    }

    public async Task<ExperienceViewModel> GetByIdAsync(long id)
    {
        var existExperience = (await repository.GetByIdAsync(experiencetable, id))
          ?? throw new CustomException(404, "No experience");

        return mapper.Map<ExperienceViewModel>(existExperience);
    }

    public async Task<ExperienceViewModel> UpdateAsync(long id, ExperienceUpdateModel experience)
    {
        var existUser = await userService.GetByIdAsync(experience.UserId);
        var existCompany = await companyService.GetByIdAsync(experience.CompanyId);

        var existExperience = (await repository.GetByIdAsync(experiencetable, id))
            ?? throw new CustomException(404, "No experience");
        var mapped = mapper.Map<ExperienceViewModel>(existExperience);

        mapped.User = existUser;
        mapped.Company = existCompany;

        return mapped;
    }
}
