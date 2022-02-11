using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS91892.Data.Repositories;

/// <inheritdoc></inheritdoc>
public class AlbumRepository : IAlbumRepository
{
    private bool disposedValue;

    /// <inheritdoc></inheritdoc>
    public Task CreateAsync(Album model)
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc></inheritdoc>
    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc></inheritdoc>
    public Task<IList<Album>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc></inheritdoc>
    public Task<IList<Album>?> GetAllAsync(Func<Album, bool> predicate)
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
    /// <inheritdoc></inheritdoc>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~AlbumRepository()
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
