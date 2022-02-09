namespace AS91892.Data.Repositories;

/// <summary>
/// Repository to manage <see cref="Artist"/>'s
/// </summary>
public class ArtistRepository : IArtistRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArtistRepository"/>
    /// </summary>
    public ArtistRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Backing database for <see cref="ArtistRepository"/>
    /// </summary>
    private ApplicationDbContext Context { get; }

    /// <inheritdoc></inheritdoc>
    public async Task CreateAsync(Artist model)
    {
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
    public async Task<IList<Artist>?> GetAll(Predicate<Artist> predicate)
    {
        return null; // TODO: finish method
    }
    /// <inheritdoc></inheritdoc>
    public async Task<IList<Artist>> GetAllAsync()
    {
        return await Task.FromResult(Context.Artists.ToList());
    }
    /// <inheritdoc></inheritdoc>
    public async Task UpdateAsync(Guid id, Artist model)
    {
        if (id != model.Id)
        {
            throw new ArgumentException("The id passed in the first parameter is not the same as the containing model", nameof(id));
        }

    }
}
