namespace AS91892.Data.Repositories;

/// <summary>
/// Repository to manage <see cref="Artist"/>'s
/// </summary>
public class ArtistRepository : BaseRepository<ArtistRepository>, IArtistRepository
{
    /// <inheritdoc></inheritdoc>/>
    public ArtistRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<int> CountAsync()
    {
        return await Context.Artists.CountAsync();
    }

    /// <inheritdoc></inheritdoc>
    public async Task CreateAsync(Artist model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Context.Artists.Add(model);
        await Context.SaveChangesAsync().ConfigureAwait(false); // stop deadlocks
    }
    /// <inheritdoc></inheritdoc>
    public async Task DeleteAsync(Guid id)
    {
        var artist = await Context.Artists.FindAsync(id).ConfigureAwait(false);

        if (artist is null)
        {
            return;
        }

        Context.Artists.Remove(artist);

        await Context.SaveChangesAsync().ConfigureAwait(false);
    }
    /// <inheritdoc></inheritdoc>
    public async Task<IList<Artist>> GetAllAsync(Func<Artist, bool> predicate)
    {
        return await Task.FromResult(Context.Artists.Where(predicate).ToList());
    }
    /// <inheritdoc></inheritdoc>
    public async Task<IList<Artist>> GetAllAsync()
    {
        return await Task.FromResult(Context.Artists.ToList());
    }
    /// <inheritdoc></inheritdoc>
    public async Task<Artist?> GetAsync(Guid id)
    {
        return await Context.Artists.FindAsync(id);
    }

    /// <inheritdoc></inheritdoc>
    public async Task UpdateAsync(Guid id, Artist model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model)); // null guard

        Assertion.AssertIdIsSame(id, model);

        Context.Artists.Update(model);

        await Context.SaveChangesAsync();
    }
}
