namespace AS91892.Data.Repositories;

/// <summary>
/// Data repository for <see cref="RecordLabel"/>'s
/// </summary>
public class LabelRepository : BaseRepository<LabelRepository>, ILabelRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LabelRepository"/>
    /// </summary>
    /// <param name="context">Database context</param>
    public LabelRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <inheritdoc></inheritdoc>
    public async Task CreateAsync(RecordLabel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        
        Context.RecordLabels.Add(model);

        await Context.SaveChangesAsync().ConfigureAwait(false);
    }
    /// <inheritdoc></inheritdoc>
    public async Task DeleteAsync(Guid id)
    {
        var model = await Context.RecordLabels.FindAsync(id).ConfigureAwait(false);

        if (model is null)
        {
            return;
        }

        Context.RecordLabels.Remove(model);

        await Context.SaveChangesAsync().ConfigureAwait(false);

    }
    /// <inheritdoc></inheritdoc>
    public Task<IList<RecordLabel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc></inheritdoc>
    public Task<IList<RecordLabel>> GetAllAsync(Func<RecordLabel, bool> predicate)
    {
        throw new NotImplementedException();
    }
    /// <inheritdoc></inheritdoc>
    public Task<RecordLabel?> GetAsync(Guid id)
    {
        return Context.RecordLabels.FindAsync(id).AsTask();
    }
    /// <inheritdoc></inheritdoc>
    public Task UpdateAsync(Guid id, RecordLabel model)
    {
        throw new NotImplementedException();
    }
}
