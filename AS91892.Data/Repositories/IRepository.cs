namespace AS91892.Data;

/// <summary>
/// Generic repository for data models
/// </summary>
/// <typeparam name="TModel">The model to create, modify, delete and fufill other duties</typeparam>
/// <typeparam name="TModelID">The id type that the model uses to uniquely identify itself</typeparam>
public interface IRepository<TModel, TModelID> : IDisposable
    where TModel : BaseEntity
    where TModelID : notnull
{
    /// <summary>
    /// Gets all of the <typeparamref name="TModel"/> in the <see cref="IRepository{TModel,TModelID}"/>
    /// </summary>
    /// <returns>Every model in the <see cref="IRepository{TModel,TModelID}"/></returns>
    Task<IList<TModel>> GetAllAsync();


    /// <summary>
    /// Gets a <typeparamref name="TModel"/> by its identifier of <typeparamref name="TModelID"/>
    /// </summary>
    /// <param name="id">The id to search for</param>
    /// <returns>A <see cref="Task{TResult}"/> where <typeparamref name="TModel"/> could be <see langword="null"/></returns>
    Task<TModel?> GetAsync(TModelID id);

    /// <summary>
    /// Gets all of <typeparamref name="TModel"/> in the <see cref="IRepository{TModel,TModelID}"/> that match the condition passed
    /// </summary>
    /// <param name="predicate">Condition of the items to retrieve</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    Task<IList<TModel>> GetAllAsync(Func<TModel, bool> predicate);
    /// <summary>
    /// Creates a <typeparamref name="TModel"/> in the <see cref="IRepository{TModel,TModelID}"/>
    /// </summary>
    /// <param name="model">The model to create</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task CreateAsync(TModel model);

    /// <summary>
    /// Updates a <typeparamref name="TModel"/> in the <see cref="IRepository{TModel,TModelID}"/>
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id">The id in the database used to update</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task UpdateAsync(TModelID id, TModel model);

    /// <summary>
    /// Gets the number of records in the database table
    /// </summary>
    /// <returns>The number of records in the database table</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Removes a <typeparamref name="TModel"/> in the <see cref="IRepository{TModel,TModelID}"/>
    /// </summary>
    /// <param name="id">The id of the <typeparamref name="TModel"/> to delete</param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    Task DeleteAsync(TModelID id);
}
