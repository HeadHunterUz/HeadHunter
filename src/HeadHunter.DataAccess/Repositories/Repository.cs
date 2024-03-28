using Npgsql;
using Dapper;

namespace HeadHunter.DataAccess.Repositories;

public class Repository<TEntity>(string connectionString) : IRepository<TEntity>
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(string tableName)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = $"SELECT * FROM {tableName}";
            return await connection.QueryAsync<TEntity>(query);
        }
    }

    public async Task<TEntity> GetByIdAsync(string tableName, int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<TEntity>(query, new { Id = id }) 
                ?? throw new NullReferenceException("Could not find entity with id: " + id);
        }
    }

    public async Task InsertAsync(string tableName, TEntity entity)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var parameters = GetParameters(entity);
            string columns = string.Join(", ", parameters.Keys);
            string values = string.Join(", ", parameters.Values);

            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            await connection.ExecuteAsync(query, entity);
        }
    }

    public async Task UpdateAsync(string tableName, TEntity entity)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var parameters = GetParameters(entity);
            string updateColumns = string.Join(", ", parameters.Select(p => $"{p.Key} = {p.Value}"));

            string query = $"UPDATE {tableName} SET {updateColumns} WHERE Id = @Id";
            await connection.ExecuteAsync(query, entity);
        }
    }

    public async Task DeleteAsync(string tableName, int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = $"DELETE FROM {tableName} WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }

    private Dictionary<string, string> GetParameters(TEntity entity)
    {
        var parameters = new Dictionary<string, string>();
        var properties = typeof(TEntity).GetProperties();

        foreach (var property in properties)
        {
            var propertyName = property.Name;
            var parameterName = $"@{propertyName}";
            parameters.Add(propertyName, parameterName);
        }

        return parameters;
    }
}