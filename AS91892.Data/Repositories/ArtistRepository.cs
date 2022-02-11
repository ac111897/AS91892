namespace AS91892.Data.Repositories;

/// <summary>
/// Repository to manage <see cref="Artist"/>'s
/// </summary>
public class ArtistRepository : IArtistRepository
{
    private bool disposedValue;

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
    public async Task<IList<Artist>?> GetAllAsync(Func<Artist, bool> predicate)
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

        if (id != model.Id)
        {
            throw new ArgumentException("The id passed in the first parameter is not the same as the containing model", nameof(id));
        }
        
    }
    /// <inheritdoc></inheritdoc>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Context.Dispose();
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~ArtistRepository()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }
    /// <inheritdoc></inheritdoc>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
