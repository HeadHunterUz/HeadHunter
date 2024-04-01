using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.Companies;
using HeadHunter.Services.Services.Jobs;

namespace HeadHunter.Services.Services.JobVacancies;

public class JobVacancyService : IJobVacancyService
{
    private IMapper _mapper;
    private IRepository<JobVacancy> _repository;
    private readonly string _vacancyTable = Constants.JobVacancyTableName;

    public JobVacancyService(IMapper mapper, IRepository<JobVacancy> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<JobVacancyViewModel> CreateAsync(JobVacancyCreateModel vacancy)
    {
        var existVacancy = (await _repository.GetAllAsync(_vacancyTable))
            .FirstOrDefault(v => v.JobId == vacancy.JobId);

        if (existVacancy != null)
            throw new CustomException(409, "Vacancy already exists");

        var created = _mapper.Map<JobVacancy>(vacancy);
        created.Id = await GenerateNewId();
        await _repository.InsertAsync(_vacancyTable, created);

        return _mapper.Map<JobVacancyViewModel>(created);
    }

    public async Task<long> GenerateNewId()
    {
        var vacancies = await _repository.GetAllAsync(_vacancyTable);
        var maxId = vacancies.Any() ? vacancies.Max(v => v.Id) : 0;
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existVacancy = await _repository.GetByIdAsync(_vacancyTable, id)
            ?? throw new CustomException(404, "Vacancy not found");

        await _repository.DeleteAsync(_vacancyTable, id);

        return true;
    }

    public async Task<IEnumerable<JobVacancyViewModel>> GetAllAsync()
    {
        var vacancies = await _repository.GetAllAsync(_vacancyTable);
        return _mapper.Map<IEnumerable<JobVacancyViewModel>>(vacancies);
    }

    public async Task<JobVacancyViewModel> GetByIdAsync(long id)
    {
        var existVacancy = await _repository.GetByIdAsync(_vacancyTable, id)
           ?? throw new CustomException(404, "Vacancy not found");

        return _mapper.Map<JobVacancyViewModel>(existVacancy);
    }

    public async Task<JobVacancyViewModel> UpdateAsync(long id, JobVacancyUpdateModel vacancy)
    {
        var existVacancy = await _repository.GetByIdAsync(_vacancyTable, id)
           ?? throw new CustomException(404, "Vacancy not found");

        var mappedVacancy = _mapper.Map(vacancy, existVacancy);
        var updatedVacancy = await _repository.UpdateAsync(_vacancyTable, mappedVacancy);

        return _mapper.Map<JobVacancyViewModel>(updatedVacancy);
    }
}