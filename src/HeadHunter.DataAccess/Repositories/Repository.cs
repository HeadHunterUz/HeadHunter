using Dapper;
using HeadHunter.DataAccess.IRepositories;
using Npgsql;

namespace HeadHunter.DataAccess.Repositories
{
    public class Repository<TEntity>(string connectionString) : IRepository<TEntity>
    {
        #region Public Methods

        /// <summary>
        /// Retrieves all entities from the specified table asynchronously.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(string tableName)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = $"SELECT * FROM {tableName}";
                return await connection.QueryAsync<TEntity>(query);
            }
        }

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved entity.</returns>
        public async Task<TEntity> GetByIdAsync(string tableName, long id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = $"SELECT * FROM {tableName} WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<TEntity>(query, new { Id = id }) ?? throw new NullReferenceException("Could not find entity with id: " + id);
            }
        }

        /// <summary>
        /// Inserts an entity into the specified table asynchronously.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the inserted entity.</returns>
        public async Task<TEntity> InsertAsync(string tableName, TEntity entity)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var parameters = GetParameters(entity);
                string columns = string.Join(", ", parameters.Keys);
                string values = string.Join(", ", parameters.Values);

                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values}) RETURNING *";
                return await connection.QuerySingleOrDefaultAsync<TEntity>(query, entity);
            }
        }

        /// <summary>
        /// Updates an entity in the specified table asynchronously.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated entity.</returns>
        public async Task<TEntity> UpdateAsync(string tableName, TEntity entity)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var parameters = GetParameters(entity);
                string updateColumns = string.Join(", ", parameters.Select(p => $"{p.Key} = {p.Value}"));

                string query = $"UPDATE {tableName} SET {updateColumns} WHERE Id = @Id RETURNING *";
                return await connection.QuerySingleOrDefaultAsync<TEntity>(query, entity);
            }
        }

        /// <summary>
        /// Deletes an entity asynchronously by its ID.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(string tableName, long id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = $"DELETE FROM {tableName} WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the parameter names for the entity properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A dictionary of property names and their corresponding parameter names.</returns>
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

        #endregion
    }
}