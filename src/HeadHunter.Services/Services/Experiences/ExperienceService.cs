using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;
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
    public readonly string experienceTable = Constants.ExperienceTableName;
    public readonly string usertable = Constants.UserTableName;
    public readonly string companyTable = Constants.CompanyTableName;

    public ExperienceService(IMapper mapper, IUserService userService, ICompanyService companyService, IRepository<Experience> repository)
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

        var existExperience = (await repository.GetAllAsync(experienceTable))
            .FirstOrDefault(a => a.UserId == experience.UserId && a.CompanyId == experience.CompanyId);

        if (existExperience != null)
            throw new CustomException(409, "Experience already exists");

        var mapped = mapper.Map<Experience>(experience);
        var created = await repository.InsertAsync(experienceTable, mapped);

        return new ExperienceViewModel
        {
            Id = created.Id,
            User = existUser,
            Company = existCompany
        };
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existExperience = (await repository.GetByIdAsync(companyTable, id))
           ?? throw new CustomException(404, "No experience");

        if (existExperience.IsDeleted)
            throw new CustomException(410, "Experience is already deleted");

        await repository.DeleteAsync(experienceTable, id);

        return true;
    }

    public async Task<IEnumerable<ExperienceViewModel>> GetAllAsync()
    {
        var Experiences = (await repository.GetAllAsync(experienceTable))
           .Where(a => !a.IsDeleted);

        return mapper.Map<IEnumerable<ExperienceViewModel>>(Experiences);
    }

    public async Task<ExperienceViewModel> GetByIdAsync(long id)
    {
        var existExperience = (await repository.GetByIdAsync(experienceTable, id))
          ?? throw new CustomException(404, "No experience");

        return mapper.Map<ExperienceViewModel>(existExperience);
    }

    public async Task<ExperienceViewModel> UpdateAsync(long id, ExperienceUpdateModel experience)
    {
        var existUser = await userService.GetByIdAsync(experience.UserId);
        var existCompany = await companyService.GetByIdAsync(experience.CompanyId);

        var existExperience = (await repository.GetByIdAsync(experienceTable, id))
            ?? throw new CustomException(404, "No experience");
        var mapped = mapper.Map<ExperienceViewModel>(existExperience);

        mapped.User = existUser;
        mapped.Company = existCompany;

        return mapped;
    }
}
