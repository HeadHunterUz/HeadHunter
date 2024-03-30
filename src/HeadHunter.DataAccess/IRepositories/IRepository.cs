namespace HeadHunter.DataAccess.IRepositories;

/// <summary>
/// Represents a generic repository interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity>
{
    /// <summary>
    /// Inserts an entity into the specified table asynchronously.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="entity">The entity to insert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InsertAsync(string tableName, TEntity entity);

    /// <summary>
    /// Updates an entity in the specified table asynchronously.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(string tableName, TEntity entity);

    /// <summary>
    /// Deletes an entity asynchronously by its ID.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(string tableName, int id);

    /// <summary>
    /// Retrieves all entities from the specified table asynchronously.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(string tableName);

    /// <summary>
    /// Retrieves an entity by its ID asynchronously.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved entity.</returns>
    Task<TEntity> GetByIdAsync(string tableName, int id);
    object SelectAsQueryable();
}