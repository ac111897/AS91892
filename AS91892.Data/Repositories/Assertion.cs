namespace AS91892.Data.Repositories;

/// <summary>
/// Asserts conditions and throws if it doesn't meet them
/// </summary>
internal class Assertion
{
    /// <summary>
    /// Asserts that the unique identifiers match before performing an operation
    /// </summary>
    /// <typeparam name="TEntity">The entity to check for</typeparam>
    /// <param name="id">The id to match against</param>
    /// <param name="entity">The entity to match against</param>
    /// <exception cref="ArgumentException">Exception thrown if they do not match</exception>
    internal static void AssertIdIsSame<TEntity>(Guid id, TEntity entity) where TEntity : BaseEntity
    {
        if (id != entity.Id)
        {
            throw new ArgumentException("The id passed in is not the same as the entities id");
        }
    }
}
