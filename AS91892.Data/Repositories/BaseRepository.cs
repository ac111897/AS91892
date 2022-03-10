using Microsoft.EntityFrameworkCore;

namespace AS91892.Data.Repositories;

/// <summary>
/// Serves as the base repository to inherit from, this will provide our <see cref="ApplicationDbContext"/> and will implement our dipose pattern
/// </summary>
public abstract class BaseRepository<T> : IDisposable
{
    /// <summary>
    /// The name of the table in sql
    /// </summary>
    protected string TableName { get; set; }
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
        TableName = Context!.Model.FindEntityType(typeof(T))!.GetTableName()!;
        Context = context;
    }

    /// <inheritdoc></inheritdoc>
    public async Task<int> CountAsync()
    {
        return 0;
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
