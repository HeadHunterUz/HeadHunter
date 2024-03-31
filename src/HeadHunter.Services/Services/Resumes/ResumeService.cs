using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Exceptions;

namespace HeadHunter.Services.Services.Resumes;

public class ResumeService : IResumeService
{
    private IMapper mapper;
    private IRepository<Resume> repository;
    private IUserService userService;
    public readonly string resumetable = Constants.ResumeTableName;
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

        var existResume = (await repository.GetAllAsync(resumetable))
            .FirstOrDefault(a => a.UserId == resume.UserId);

        if (existResume != null)
            throw new CustomException(409, " No Resume");

        var completed = mapper.Map<Resume>(resume);
        await repository.InsertAsync(resumetable, completed);

        var viewModel = mapper.Map<ResumeViewModel>(completed);
        viewModel.User = mapper.Map<UserViewModel>(existUser);

        return viewModel;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existResume = (await repository.GetByIdAsync(resumetable, id))
            ?? throw new CustomException(404, "Resume not found");

        await repository.DeleteAsync(resumetable, id);

        return true;
    }

    public async Task<IEnumerable<ResumeViewModel>> GetAllAsync()
    {
        var Resumes = (await repository.GetAllAsync(resumetable))
            .Where(a => !a.IsDeleted);

        return mapper.Map<IEnumerable<ResumeViewModel>>(Resumes);
    }

    public async Task<ResumeViewModel> GetByIdAsync(long id)
    {
        var existResume = (await repository.GetByIdAsync(resumetable, id))
          ?? throw new CustomException(404, "Resume not found");

        return mapper.Map<ResumeViewModel>(existResume);
    }

    public async Task<ResumeViewModel> UpdateAsync(long id, ResumeUpdateModel resume)
    {
        var existUser = await userService.GetByIdAsync(resume.UserId);

        var existResume = (await repository.GetByIdAsync(resumetable, id))
            ?? throw new CustomException(404, "Resume not found");
        var mapped = mapper.Map<ResumeViewModel>(existResume);

        mapped.User = existUser;

        return mapped;
    }
}
