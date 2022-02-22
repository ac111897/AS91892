namespace AS91892.Core.MockData;

/// <summary>
/// Interface to generate mock data for out test environments
/// </summary>
/// <typeparam name="TEntity">Represents the type of record we are mocking for</typeparam>
public interface IMockDataResolver<TEntity> where TEntity : Data.Entities.BaseEntity
{
    /// <summary>
    /// Generates a iterable <see cref="IEnumerable{T}"/> where T is <typeparamref name="TEntity"/> for mock data purposes
    /// </summary>
    /// <param name="amount">The amount of records you want to generate</param>
    /// <returns><see cref="IEnumerable{T}"/> of <typeparamref name="TEntity"/></returns>
    public IEnumerable<TEntity> GenerateMock(int amount);
}
