namespace AS91892.Data.Repositories;

/// <summary>
/// Data repository for <see cref="Song"/>
/// </summary>
public class SongRepository : BaseRepository<SongRepository>, ISongRepository
{
    /// <inheritdoc></inheritdoc>/>
    public IQueryable<Song> Source => Context.Songs.AsNoTracking();
    /// <inheritdoc></inheritdoc>/>
    public SongRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <inheritdoc></inheritdoc>
    public async Task<int> CountAsync(Expression<Func<Song, bool>> predicate)
    {
        return await Context.Songs.CountAsync(predicate);
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<int> CountAsync()
    {
        return await Context.Songs.CountAsync();
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task CreateAsync(Song model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Context.Songs.Add(model);

        await Context.SaveChangesAsync();
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task DeleteAsync(Guid id)
    {
        var model = await Context.Songs.FindAsync(id);

        if (model is null)
        {
            return;
        }

        Context.Songs.Remove(model);

        await Context.SaveChangesAsync();
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<IList<Song>> GetAllAsync()
    {
        return await Task.FromResult(Context.Songs.ToList());
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<IList<Song>> GetAllAsync(Func<Song, bool> predicate)
    {
        return await Task.FromResult(Context.Songs.Where(predicate).ToList());
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<Song?> GetAsync(Guid id)
    {
        return await Context.Songs
            .Include(x => x.Cover)
            .Include(x => x.Features)
            .Include(x => x.Genre)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task UpdateAsync(Guid id, Song model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Assertion.AssertIdIsSame(id, model);

        Context.Songs.Update(model);

        await Context.SaveChangesAsync();
    }
}
