using AutoMapper;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Services.DTOs.Admins.Dtos;

namespace HeadHunter.Services.Services.Admins
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> _repository;
        private readonly IEnumerable<Admin> _admins;
        private readonly IMapper _mapper;
        private readonly string _table = "admins";

        public AdminService(IMapper mapper, IRepository<Admin> repository)
        {
            _repository = repository;
            _mapper = mapper;
            _admins = _repository.GetAllAsync(_table).Result.ToList();
        }

        public async Task<AdminViewModel> CreateAsync(AdminCreateModel model)
        {
            var existPhone = _admins.FirstOrDefault(a => a.Phone == model.Phone);
            if (existPhone is not null)
                throw new Exception($"Admin with Phone {model.Phone} already exists");

            var existEmail = _admins.FirstOrDefault(a => a.Email == model.Email);
            if (existEmail is not null)
                throw new Exception($"Admin with Email {model.Email} already exists");

            var admin = _mapper.Map<Admin>(model);
            admin.Id = GenerateNewId();
            _admins.ToList().Add(admin);
            await _repository.InsertAsync(_table, admin);
            return _mapper.Map<AdminViewModel>(admin);
        }

        private long GenerateNewId()
        {
            long maxId = _admins.ToList().Max(a => a.Id);
            return maxId + 1;
        }

        public async Task<AdminViewModel> UpdateAsync(long id, AdminUpdateModel model)
        {
            var exist = _admins.FirstOrDefault(a => a.Id == id);
            if (exist is null)
                throw new Exception("This admin is not found");

            _admins.ToList().Remove(exist);
            exist.UpdatedAt = DateTime.Now;
            exist.Email = model.Email;
            exist.Phone = model.Phone;
            exist.Password = model.Password;
            await _repository.UpdateAsync(_table, exist);
            return _mapper.Map<AdminViewModel>(exist);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var exist = _admins.FirstOrDefault(a => a.Id == id);
            if (exist is null)
                throw new Exception("This admin is not found");

            exist.IsDeleted = true;
            _admins.ToList().Remove(exist);
            await _repository.DeleteAsync(_table, id);
            return true;
        }

        public IEnumerable<AdminViewModel> GetAllAsEnumerable()
        {
            return _mapper.Map<IEnumerable<AdminViewModel>>(_admins);
        }

        public IQueryable<AdminViewModel> GetAllAsQueryable()
        {
            return _mapper.ProjectTo<AdminViewModel>(_admins.AsQueryable());
        }

        public async Task<AdminViewModel> GetByIdAsync(long id)
        {
            var admin = await _repository.GetByIdAsync(_table, id);
            return _mapper.Map<AdminViewModel>(admin);
        }
    }
}