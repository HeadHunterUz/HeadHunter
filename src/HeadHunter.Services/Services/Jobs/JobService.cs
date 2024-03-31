using AutoMapper;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Services.Services.Jobs;

public class JobService : IJobService
{
    IRepository<Job> repository;
    private List<Job> jobs;
    private IMapper mapper;
    private string table = "jobs";
    public JobService(IMapper mapper, IRepository<Job> repository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.jobs = repository.GetAllAsync(table).Result.ToList();
    }
    public async Task<JobViewModel> CreateAsync(JobCreateModel model)
    {
        var job = mapper.Map<Job>(model);
        job.Id = await GenerateNewId();
        jobs.Add(job);
        return await Task.FromResult(mapper.Map<JobViewModel>(job));
    }
    private async Task<long> GenerateNewId()
    {
        long maxId = jobs.Max(a => a.Id);
        return maxId + 1;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = jobs.FirstOrDefault(x => x.Id == id);
        if (exist is null)
            throw new Exception("This job is not found");

        exist.IsDeleted = true;
        jobs.Remove(exist);
        await repository.DeleteAsync(table, id);
        return true;
    }

    public IEnumerable<JobViewModel> GetAllAsEnumerable()
    {
        return mapper.Map<IEnumerable<JobViewModel>>(jobs);
    }

    public IQueryable<JobViewModel> GetAllAsQueyable()
    {
        return mapper.Map<IQueryable<JobViewModel>>(jobs);
    }

    public async Task<JobViewModel> GetByIdAsync(long id)
    {
        var exist = jobs.FirstOrDefault(x => x.Id == id);
        if (exist is null)
            throw new Exception("This job is not found");

        return await Task.FromResult(mapper.Map<JobViewModel>(exist));
    }

    public async Task<JobViewModel> UpdateAsync(long id, JobUpdateModel model)
    {
        var exist = jobs.FirstOrDefault(x => x.Id == id);
        if (exist is null)
            throw new Exception("This job is not found");

        exist.UpdatedAt = DateTime.UtcNow;
        exist.Name = model.Name;
        jobs.Remove(exist);
        await repository.DeleteAsync(table, id);
        return await Task.FromResult(mapper.Map<JobViewModel>(exist));
    }
}
