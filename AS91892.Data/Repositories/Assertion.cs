namespace AS91892.Data.Repositories;

internal class Assertion
{
    internal static void AssertIdIsSame<TEntity>(Guid id, TEntity entity) where TEntity : BaseEntity
    {
        if (id != entity.Id)
        {
            throw new ArgumentException("The id passed in is not the same as the entities id");
        }
    }
}
