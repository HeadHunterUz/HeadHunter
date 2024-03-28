
namespace HeadHunter.DataAccess.Repositories
{
    public interface IRepository<TEntity>
    {
        Task DeleteAsync(string tableName, int id);
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName);
        Task<TEntity> GetByIdAsync(string tableName, int id);
        Task InsertAsync(string tableName, TEntity entity);
        Task UpdateAsync(string tableName, TEntity entity);
    }
}