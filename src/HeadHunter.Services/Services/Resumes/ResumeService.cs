using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;
using HeadHunter.Services.Services.Users;

namespace HeadHunter.Services.Services.Resumes;

public class ResumeService : IResumeService
{
    private IMapper _mapper;
    private IRepository<Resume> _repository;
    private readonly string _resumeTable = Constants.ResumeTableName;

    public ResumeService(IMapper mapper, IRepository<Resume> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ResumeViewModel> CreateAsync(ResumeCreateModel resume)
    {
        var existResume = (await _repository.GetAllAsync(_resumeTable))
            .FirstOrDefault(a => a.UserId == resume.UserId);

        if (existResume != null)
            throw new CustomException(409, "Resume already exists");

        var created = _mapper.Map<Resume>(resume);
        created.Id = await GenerateNewId();
        await _repository.InsertAsync(_resumeTable, created);

        return _mapper.Map<ResumeViewModel>(created);
    }

    public async Task<long> GenerateNewId()
    {
        var resumes = await _repository.GetAllAsync(_resumeTable);
        var maxId = resumes.Any() ? resumes.Max(r => r.Id) : 0;
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existResume = await _repository.GetByIdAsync(_resumeTable, id)
            ?? throw new CustomException(404, "Resume not found");

        await _repository.DeleteAsync(_resumeTable, id);

        return true;
    }

    public async Task<IEnumerable<ResumeViewModel>> GetAllAsync()
    {
        var resumes = (await _repository.GetAllAsync(_resumeTable))
            .Where(a => !a.IsDeleted);

        return _mapper.Map<IEnumerable<ResumeViewModel>>(resumes);
    }

    public async Task<ResumeViewModel> GetByIdAsync(long id)
    {
        var existResume = await _repository.GetByIdAsync(_resumeTable, id)
            ?? throw new CustomException(404, "Resume not found");

        return _mapper.Map<ResumeViewModel>(existResume);
    }

    public async Task<ResumeViewModel> UpdateAsync(long id, ResumeUpdateModel resume)
    {
        var existResume = await _repository.GetByIdAsync(_resumeTable, id)
            ?? throw new CustomException(404, "Resume not found");

        var mappedResume = _mapper.Map(resume, existResume);

        return _mapper.Map<ResumeViewModel>(mappedResume);
    }
}
