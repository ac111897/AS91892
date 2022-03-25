namespace AS91892.Data.Repositories.Abstractions;

/// <summary>
/// Repository for <see cref="GenreRepository"/>
/// </summary>
public class GenreRepository : BaseRepository<GenreRepository>, IGenreRepository
{
    /// <inheritdoc></inheritdoc>/>
    public GenreRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <inheritdoc></inheritdoc>
    public async Task<int> CountAsync(Expression<Func<Genre, bool>> predicate)
    {
        return await Context.Genres.CountAsync(predicate);
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<int> CountAsync()
    {
        return await Context.Genres.CountAsync();
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task CreateAsync(Genre model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        Context.Genres.Add(model);
        await Context.SaveChangesAsync().ConfigureAwait(false);
    }
    /// <inheritdoc></inheritdoc>/>
    public async Task DeleteAsync(Guid id)
    {
        var model = await Context.Genres.FindAsync(id).ConfigureAwait(false);

        if (model is null)
        {
            return;
        }

        Context.Genres.Remove(model);

        await Context.SaveChangesAsync().ConfigureAwait(false);
    }
    /// <inheritdoc></inheritdoc>/>
    public async Task<IList<Genre>> GetAllAsync()
    {
        return await Task.FromResult(Context.Genres.ToList());
    }
    /// <inheritdoc></inheritdoc>/>
    public async Task<IList<Genre>> GetAllAsync(Func<Genre, bool> predicate)
    {
        return await Task.FromResult(Context.Genres.Where(predicate).ToList()).ConfigureAwait(false);
    }
    /// <inheritdoc></inheritdoc>/>
    public async Task<Genre?> GetAsync(Guid id)
    {
        return await Context.Genres.FindAsync(id);
    }
    /// <inheritdoc></inheritdoc>/>
    public async Task UpdateAsync(Guid id, Genre model)
    {
        ArgumentNullException.ThrowIfNull(model);

        Assertion.AssertIdIsSame(id, model);

        Context.Genres.Update(model);

        await Context.SaveChangesAsync().ConfigureAwait(false);
    }
}
