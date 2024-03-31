using AutoMapper;
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
    public readonly string experienceTable = DataAccess.Constants.ExperienceTableName;
    public readonly string usertable = DataAccess.Constants.UserTableName;
    public readonly string companyTable = DataAccess.Constants.CompanyTableName;

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
        var experiences = await repository.GetAllAsync(experienceTable);
        var experienceTasks = experiences
        .Where(a => !a.IsDeleted)
            .Select(async app =>
            {
                var existUserTask = userService.GetByIdAsync(app.UserId);
                var existCompanyTask = companyService.GetByIdAsync(app.CompanyId);
                var mapped = mapper.Map<ExperienceViewModel>(app);

                mapped.Id = app.Id;

                var existUser = await existUserTask ?? new UserViewModel();
                var existJobVacancy = await existCompanyTask ?? new CompanyViewModel();

                mapped.User = existUser;
                mapped.Company = existJobVacancy;

                return mapped;
            });

        var mappedExperiences = await Task.WhenAll(experienceTasks);
        return mappedExperiences;
    }

    public async Task<ExperienceViewModel> GetByIdAsync(long id)
    {
        var existExperience = (await repository.GetByIdAsync(experienceTable, id))
          ?? throw new CustomException(404, "No experience");
        if (existExperience.IsDeleted)
            throw new CustomException(410, "Experience is already deleted");

        var existUser = await userService.GetByIdAsync(existExperience.UserId);
        var existCompany = await companyService.GetByIdAsync(existExperience.CompanyId);

        var mapped = mapper.Map<ExperienceViewModel>(existExperience);

        mapped.Id = existExperience.Id;
        mapped.User = existUser;
        mapped.Company = existCompany;

        return mapped;
    }

    public async Task<ExperienceViewModel> UpdateAsync(long id, ExperienceUpdateModel experience)
    {
        var existUser = await userService.GetByIdAsync(experience.UserId);
        var existCompany = await companyService.GetByIdAsync(experience.CompanyId);

        var existExperience = (await repository.GetByIdAsync(experienceTable, id))
            ?? throw new CustomException(404, "No experience");

        var mapped = mapper.Map(experience, existExperience);
        var updatedExperience = await repository.UpdateAsync(usertable, mapped);

        return new ExperienceViewModel
        {
            Id = updatedExperience.Id,
            Company = existCompany,
            User = existUser,
            StartTime = updatedExperience.StartTime,
            EndTime = updatedExperience.EndTime,
            JobTitle = updatedExperience.JobTitle,
            Position = updatedExperience.Position,
        };
    }
}
