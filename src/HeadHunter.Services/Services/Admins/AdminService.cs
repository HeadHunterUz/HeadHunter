using AutoMapper;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.DataAccess.Repositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Services.DTOs.Admins.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.Helpers;
using HeadHunter.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Services.Services.Admins;

public class AdminService : IAdminService
{
    IRepository<Admin> repository;
    private List<Admin> admins;
    private IMapper mapper;
    private string table = "admins";
    public AdminService(IMapper mapper, IRepository<Admin> repository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.admins = repository.GetAllAsync(table).Result.ToList();
    }
    public async Task<AdminViewModel> CreateAsync(AdminCreateModel model)
    {
        var existPhone = admins.FirstOrDefault(a => a.Phone == model.Phone);
        if (existPhone is not null)
            throw new Exception($"Admin with Phone {model.Phone} is already exists");

        var existEmail = admins.FirstOrDefault(a => a.Email == model.Email);
        if (existEmail is not null)
            throw new Exception($"Admin with Email {model.Email} is already exists");

        var admin = mapper.Map<Admin>(model);
        admins.Add(admin);
        await repository.InsertAsync(table, admin);
        return mapper.Map<AdminViewModel>(model);
    }
    public async Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel model)
    {
        var exist = admins.FirstOrDefault(a => a.Id == model.Id);
        if (exist is null)
            throw new Exception("This admin is not found");

        admins.Remove(exist);
        exist.UpdatedAt = DateTime.Now;
        exist.Email = model.Email;
        exist.Phone = model.Phone;
        exist.Password = model.Password;
        await repository.UpdateAsync(table, exist);
        return mapper.Map<AdminViewModel>(model);
    }
    public async Task<bool> DeleteAsync(long id)
    {
        var exist = admins.FirstOrDefault(a => a.Id == id);
        if (exist is null)
            throw new Exception("This admin is not found");

        exist.IsDeleted = true;
        admins.Remove(exist);
        await repository.DeleteAsync(table, id);
        return true;
    }
    public IEnumerable<AdminViewModel> GetAllAsEnumerable()
    {
        return mapper.Map<IEnumerable<AdminViewModel>>(admins);
    }
    public IQueryable<AdminViewModel> GetAllAsQueryable()
    {
        return mapper.Map<IQueryable<AdminViewModel>>(admins);
    }
}
