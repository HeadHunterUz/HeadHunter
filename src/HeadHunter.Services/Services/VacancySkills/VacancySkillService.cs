using AutoMapper;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Vacancies;
using HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.JobVacancies;

namespace HeadHunter.Services.Services.VacancySkills;

public class VacancySkillService : IVacancySkillService
{
    private IMapper mapper;
    private IRepository<VacancySkill> repository;
    private IJobVacancyService jobVacancyService;
    private readonly string vacancySkillTable = DataAccess.Constants.VacancySkillTableName;


    public async Task<VacancySkillViewModel> CreateAsync(VacancySkillCreateModel vacancySkill)
    {
        var existJobVacancy = await jobVacancyService.GetByIdAsync(vacancySkill.JobVacancyId);

        var existVacancySkill = (await repository.GetAllAsync(vacancySkillTable))
            .FirstOrDefault(v => v.VacancyId == vacancySkill.JobVacancyId && v.Name == vacancySkill.Name);

        if (existVacancySkill != null)
            throw new CustomException(409, "VacancySkill is already exists");

        var created = mapper.Map<VacancySkill>(vacancySkill);
        created.Id = await GenerateNewId();

        await repository.InsertAsync(vacancySkillTable, mapper.Map<VacancySkill>(vacancySkill));

        return new VacancySkillViewModel
        {
            Id = created.Id,
            Name = vacancySkill.Name,
            JobVacancy = existJobVacancy
        };
    }

    private async Task<long> GenerateNewId()
    {
        long maxId = (await repository.GetAllAsync(vacancySkillTable)).Max(v => v.Id);
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existVacancySkill = await repository.GetByIdAsync(vacancySkillTable, id)
            ?? throw new CustomException(404, "Not found");

        await repository.DeleteAsync(vacancySkillTable, id);
        return true;
    }

    public async Task<IEnumerable<VacancySkillViewModel>> GetAllAsync()
    {
        var VacancySkills = await repository.GetAllAsync(vacancySkillTable);

        return mapper.Map<IEnumerable<VacancySkillViewModel>>(VacancySkills);
    }

    public async Task<VacancySkillViewModel> GetByIdAsync(long id)
    {
        var existVacancySkill = await repository.GetByIdAsync(vacancySkillTable, id)
            ?? throw new CustomException(404, "Not found");
        return mapper.Map<VacancySkillViewModel>(existVacancySkill);

    }

    public async Task<VacancySkillViewModel> UpdateAsync(long id, VacancySkillUpdateModel vacancySkill)
    {
        var existJobVacancy = await jobVacancyService.GetByIdAsync(vacancySkill.JobVacancyId);

        var existVacancySkill = await repository.GetByIdAsync(vacancySkillTable, id)
           ?? throw new CustomException(404, "Not found");

        var mapped = mapper.Map(vacancySkill, existVacancySkill);
        var updated = await repository.UpdateAsync(vacancySkillTable, mapped);

        return new VacancySkillViewModel
        {
            Id = id,
            JobVacancy = existJobVacancy,
            Name = updated.Name,
        };
    }
}
