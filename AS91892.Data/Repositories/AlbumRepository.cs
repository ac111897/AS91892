namespace AS91892.Data.Repositories;

/// <inheritdoc></inheritdoc>
public class AlbumRepository : BaseRepository<AlbumRepository>, IAlbumRepository
{
    /// <inheritdoc></inheritdoc>
    public AlbumRepository(ApplicationDbContext context) : base(context)
    {
    }


    /// <inheritdoc></inheritdoc>
    public async Task<int> CountAsync(Expression<Func<Album, bool>> predicate)
    {
        return await Context.Albums.CountAsync(predicate);
    }

    /// <inheritdoc></inheritdoc>
    public async Task<int> CountAsync()
    {
        return await Context.Albums.CountAsync();
    }

    /// <inheritdoc></inheritdoc>
    public async Task CreateAsync(Album model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        Context.Albums.Add(model);
        await Context.SaveChangesAsync();
    }

    /// <inheritdoc></inheritdoc>
    public async Task DeleteAsync(Guid id)
    {
        var item = await Context.Albums.FindAsync(id);

        if (item is null)
        {
            return;
        }

        Context.Albums.Remove(item);

        await Context.SaveChangesAsync();
    }

    /// <inheritdoc></inheritdoc>
    public async Task<IList<Album>> GetAllAsync()
    {
        return await Context.Albums
            .Include(x => x.AlbumCover)
            .Include(x => x.AlbumSongs)
            .ToListAsync();
    }

    /// <inheritdoc></inheritdoc>
    public async Task<IList<Album>> GetAllAsync(Func<Album, bool> predicate)
    {
        return await Task.FromResult(Context.Albums.Where(predicate).ToList());
    }

    /// <inheritdoc></inheritdoc>
    public async Task<Album?> GetAsync(Guid id)
    {
        return await Context.Albums
            .Include(x => x.AlbumCover)
            .Include(x => x.AlbumSongs)
            .ThenInclude(x => x.Features)
            .Include(x => x.AlbumSongs)
            .ThenInclude(x => x.Cover)
            .Include(x => x.AlbumSongs)
            .ThenInclude(x => x.Genre)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    /// <inheritdoc></inheritdoc>
    public async Task UpdateAsync(Guid id, Album model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Assertion.AssertIdIsSame(id, model);

        Context.Albums.Update(model);

        await Context.SaveChangesAsync();
    }
}
