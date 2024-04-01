using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.DataAccess.Repositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Entities.Users;
using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Experiences;

public class ExperienceService : IExperienceService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Experience> _repository;
    private readonly string _experienceTable;
    private readonly string _userTable;
    private readonly string _companyTable;

    public ExperienceService(IMapper mapper, IRepository<Experience> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _experienceTable = Constants.ExperienceTableName;
        _userTable = Constants.UserTableName;
        _companyTable = Constants.CompanyTableName;
    }

    public async Task<ExperienceViewModel> CreateAsync(ExperienceCreateModel experience)
    {
        var existUser = await GetUserByIdAsync(experience.UserId);
        var existCompany = await GetCompanyByIdAsync(experience.CompanyId);

        var existExperience = (await _repository.GetAllAsync(_experienceTable))
            .FirstOrDefault(a => a.UserId == experience.UserId && a.CompanyId == experience.CompanyId);

        if (existExperience != null)
            throw new CustomException(409, "Experience already exists");

        var mapped = _mapper.Map<Experience>(experience);
        mapped.Id = (await _repository.GetAllAsync(_experienceTable)).Last().Id + 1;
        var created = await _repository.InsertAsync(_experienceTable, mapped);

        return new ExperienceViewModel
        {
            Id = created.Id,
            UserId = created.UserId,
            CompanyId = created.CompanyId,
            JobTitle = created.JobTitle,
            Position = created.Position,
            StartTime = created.StartTime,
            EndTime = created.EndTime,
        };
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existExperience = await _repository.GetByIdAsync(_experienceTable, id)
            ?? throw new CustomException(404, "Experience not found");

        if (existExperience.IsDeleted)
            throw new CustomException(410, "Experience is already deleted");

        await _repository.DeleteAsync(_experienceTable, id);
        return true;
    }

    public async Task<IEnumerable<ExperienceViewModel>> GetAllAsync()
    {
        var experiences = await _repository.GetAllAsync(_experienceTable);
        var experienceTasks = experiences
            .Where(a => !a.IsDeleted)
            .Select(async app =>
            {
                var existUserTask = GetUserByIdAsync(app.UserId);
                var existCompanyTask = GetCompanyByIdAsync(app.CompanyId);
                var mapped = _mapper.Map<ExperienceViewModel>(app);

                mapped.Id = app.Id;

                var existUser = await existUserTask ?? new UserViewModel();
                var existCompany = await existCompanyTask ?? new CompanyViewModel();

                mapped.UserId = existUser.Id;
                mapped.CompanyId = existCompany.Id;

                return mapped;
            });

        var mappedExperiences = await Task.WhenAll(experienceTasks);
        return mappedExperiences;
    }

    public async Task<ExperienceViewModel> GetByIdAsync(long id)
    {
        var existExperience = await _repository.GetByIdAsync(_experienceTable, id)
            ?? throw new CustomException(404, "Experience not found");

        if (existExperience.IsDeleted)
            throw new CustomException(410, "Experience is already deleted");

        var existUser = await GetUserByIdAsync(existExperience.UserId);
        var existCompany = await GetCompanyByIdAsync(existExperience.CompanyId);

        var mapped = _mapper.Map<ExperienceViewModel>(existExperience);

        mapped.UserId = existUser.Id;
        mapped.CompanyId = existCompany.Id;

        return mapped;
    }

    public async Task<ExperienceViewModel> UpdateAsync(long id, ExperienceUpdateModel experience)
    {
        var existUser = await GetUserByIdAsync(experience.UserId);
        var existCompany = await GetCompanyByIdAsync(experience.CompanyId);

        var existExperience = await _repository.GetByIdAsync(_experienceTable, id)
            ?? throw new CustomException(404, "Experience not found");

        var mapped = _mapper.Map(experience, existExperience);
        var updatedExperience = await _repository.UpdateAsync(_userTable, mapped);

        return new ExperienceViewModel
        {
            Id = updatedExperience.Id,
            UserId = existUser.Id,
            CompanyId = existCompany.Id,
            JobTitle = updatedExperience.JobTitle,
            Position = updatedExperience.Position,
            StartTime = updatedExperience.StartTime,
            EndTime = updatedExperience.EndTime,
        };
    }

    private async Task<UserViewModel> GetUserByIdAsync(long userId)
    {
        IRepository<User> repository = new Repository<User>(_userTable);
        return _mapper.Map<UserViewModel>(await repository.GetByIdAsync(_userTable, userId));
    }

    private async Task<CompanyViewModel> GetCompanyByIdAsync(long companyId)
    {
        IRepository<Company> repository = new Repository<Company>(_companyTable);
        return _mapper.Map<CompanyViewModel>(await repository.GetByIdAsync(_companyTable, companyId));
    }
}