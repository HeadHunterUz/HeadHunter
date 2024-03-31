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
    private IMapper mapper;
    private IRepository<Resume> repository;
    private IUserService userService;
    public readonly string resumeTable = Constants.ResumeTableName;
    public readonly string usertable = Constants.UserTableName;

    public ResumeService(IMapper mapper, IUserService userService, IRepository<Resume> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.userService = userService;
    }
    public async Task<ResumeViewModel> CreateAsync(ResumeCreateModel resume)
    {
        var existUser = await userService.GetByIdAsync(resume.UserId);

        var existResume = (await repository.GetAllAsync(resumeTable))
            .FirstOrDefault(a => a.UserId == resume.UserId);

        if (existResume != null)
            throw new CustomException(409, " No Resume");

        var completed = mapper.Map<Resume>(resume);
        completed.Id = await GenerateNewId();
        await repository.InsertAsync(resumeTable, completed);

        var viewModel = mapper.Map<ResumeViewModel>(completed);
        viewModel.User = mapper.Map<UserViewModel>(existUser);

        return viewModel;
    }
    public async Task<long> GenerateNewId()
    {
        long maxId = (await repository.GetAllAsync(resumeTable)).Max(v => v.Id);
        return maxId + 1;
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var existResume = (await repository.GetByIdAsync(resumeTable, id))
            ?? throw new CustomException(404, "Resume not found");

        await repository.DeleteAsync(resumeTable, id);

        return true;
    }

    public async Task<IEnumerable<ResumeViewModel>> GetAllAsync()
    {
        var Resumes = (await repository.GetAllAsync(resumeTable))
            .Where(a => !a.IsDeleted);

        return mapper.Map<IEnumerable<ResumeViewModel>>(Resumes);
    }

    public async Task<ResumeViewModel> GetByIdAsync(long id)
    {
        var existResume = (await repository.GetByIdAsync(resumeTable, id))
          ?? throw new CustomException(404, "Resume not found");

        return mapper.Map<ResumeViewModel>(existResume);
    }

    public async Task<ResumeViewModel> UpdateAsync(long id, ResumeUpdateModel resume)
    {
        var existUser = await userService.GetByIdAsync(resume.UserId);

        var existResume = (await repository.GetByIdAsync(resumeTable, id))
            ?? throw new CustomException(404, "Resume not found");
        var mapped = mapper.Map<ResumeViewModel>(existResume);

        mapped.User = existUser;

        return mapped;
    }
}
