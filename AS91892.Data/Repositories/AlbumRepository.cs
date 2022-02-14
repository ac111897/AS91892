using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS91892.Data.Repositories;

/// <inheritdoc></inheritdoc>
public class AlbumRepository : BaseRepository<AlbumRepository>, IAlbumRepository
{
    /// <inheritdoc></inheritdoc>
    public AlbumRepository(ApplicationDbContext context) : base(context)
    {
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
    public Task<IList<Album>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc></inheritdoc>
    public Task<IList<Album>> GetAllAsync(Func<Album, bool> predicate)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc></inheritdoc>
    public Task<Album?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc></inheritdoc>
    public Task UpdateAsync(Guid id, Album model)
    {
        throw new NotImplementedException();
    }
}
