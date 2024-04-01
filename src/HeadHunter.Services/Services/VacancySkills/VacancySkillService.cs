using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Vacancies;
using HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.VacancySkills;

public class VacancySkillService : IVacancySkillService
{
    private IMapper _mapper;
    private IRepository<VacancySkill> _repository;
    private readonly string _vacancySkillTable = Constants.VacancySkillTableName;

    public VacancySkillService(IMapper mapper, IRepository<VacancySkill> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<VacancySkillViewModel> CreateAsync(VacancySkillCreateModel vacancySkill)
    {
        var existVacancySkill = (await _repository.GetAllAsync(_vacancySkillTable))
            .FirstOrDefault(v => v.VacancyId == vacancySkill.JobVacancyId && v.Name == vacancySkill.Name);

        if (existVacancySkill != null)
            throw new CustomException(409, "VacancySkill already exists");

        var created = _mapper.Map<VacancySkill>(vacancySkill);
        created.Id = await GenerateNewId();

        await _repository.InsertAsync(_vacancySkillTable, created);

        return new VacancySkillViewModel
        {
            Id = created.Id,
            Name = vacancySkill.Name,
            JobVacancyId = vacancySkill.JobVacancyId
        };
    }

    private async Task<long> GenerateNewId()
    {
        var vacancySkills = await _repository.GetAllAsync(_vacancySkillTable);
        var maxId = vacancySkills.Any() ? vacancySkills.Max(v => v.Id) : 0;
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existVacancySkill = await _repository.GetByIdAsync(_vacancySkillTable, id)
            ?? throw new CustomException(404, "VacancySkill not found");

        await _repository.DeleteAsync(_vacancySkillTable, id);
        return true;
    }

    public async Task<IEnumerable<VacancySkillViewModel>> GetAllAsync()
    {
        var vacancySkills = await _repository.GetAllAsync(_vacancySkillTable);

        return _mapper.Map<IEnumerable<VacancySkillViewModel>>(vacancySkills);
    }

    public async Task<VacancySkillViewModel> GetByIdAsync(long id)
    {
        var existVacancySkill = await _repository.GetByIdAsync(_vacancySkillTable, id)
            ?? throw new CustomException(404, "VacancySkill not found");

        return _mapper.Map<VacancySkillViewModel>(existVacancySkill);
    }

    public async Task<VacancySkillViewModel> UpdateAsync(long id, VacancySkillUpdateModel vacancySkill)
    {
        var existVacancySkill = await _repository.GetByIdAsync(_vacancySkillTable, id)
            ?? throw new CustomException(404, "VacancySkill not found");

        var mappedVacancySkill = _mapper.Map(vacancySkill, existVacancySkill);
        var updated = await _repository.UpdateAsync(_vacancySkillTable, mappedVacancySkill);

        return new VacancySkillViewModel
        {
            Id = id,
            Name = updated.Name,
            JobVacancyId = updated.VacancyId
        };
    }
}
