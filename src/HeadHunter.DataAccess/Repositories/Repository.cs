using Dapper;
using HeadHunter.DataAccess.IRepositories;
using Npgsql;

namespace HeadHunter.DataAccess.Repositories;

/// <summary>
/// Generic repository implementation for performing CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="Repository{TEntity}"/> class with the specified connection string.
/// </remarks>
/// <param name="connectionString">The database connection string.</param>
public class Repository<TEntity>(string connectionString) : IRepository<TEntity>
{
    #region Public Methods

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync(string tableName)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = $"SELECT * FROM {tableName}";
            return await connection.QueryAsync<TEntity>(query);
        }
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(string tableName, int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<TEntity>(query, new { Id = id }) ?? throw new NullReferenceException("Could not find entity with id: " + id);
        }
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task DeleteAsync(string tableName, int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = $"DELETE FROM {tableName} WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Retrieves the database column names and associated parameter names for the entity's properties.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A dictionary containing the column names as keys and parameter names as values.</returns>
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

    #endregion Private Methods
}