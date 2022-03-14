using Microsoft.EntityFrameworkCore;

namespace AS91892.Data.Repositories;

/// <summary>
/// Serves as the base repository to inherit from, this will provide our <see cref="ApplicationDbContext"/> and will implement our dipose pattern
/// </summary>
public abstract class BaseRepository<T> : IDisposable
{
    private bool disposedValue;

    /// <summary>
    /// Backing database for the repository
    /// </summary>
    protected ApplicationDbContext Context { get; }

    /// <summary>
    /// Initializes a new instance of <typeparamref name="T"/> class
    /// </summary>
    /// <param name="context"></param>
    public BaseRepository(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        Context = context;
    }

    /// <inheritdoc></inheritdoc>/>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            disposedValue = true;
        }
    }

    /// <inheritdoc></inheritdoc>/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
