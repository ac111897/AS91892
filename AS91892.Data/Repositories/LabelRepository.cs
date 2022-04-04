namespace AS91892.Data.Repositories;

/// <summary>
/// Data repository for <see cref="RecordLabel"/>'s
/// </summary>
public class LabelRepository : BaseRepository<LabelRepository>, ILabelRepository
{
    /// <inheritdoc></inheritdoc>/>
    public IQueryable<RecordLabel> Source => Context.RecordLabels.AsNoTracking();

    /// <inheritdoc></inheritdoc>/>
    public LabelRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <inheritdoc></inheritdoc>
    public async Task<int> CountAsync(Expression<Func<RecordLabel, bool>> predicate)
    {
        return await Context.RecordLabels.CountAsync(predicate);
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<int> CountAsync()
    {
        return await Context.RecordLabels.CountAsync();
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
    public async Task<IList<RecordLabel>> GetAllAsync()
    {
        return await Task.FromResult(Context.RecordLabels.ToList());
    }
    /// <inheritdoc></inheritdoc>
    public async Task<IList<RecordLabel>> GetAllAsync(Func<RecordLabel, bool> predicate)
    {
        return await Task.FromResult(Context.RecordLabels.Where(predicate).ToList());
    }
    /// <inheritdoc></inheritdoc>
    public Task<RecordLabel?> GetAsync(Guid id)
    {
        return Context.RecordLabels.FindAsync(id).AsTask();
    }
    /// <inheritdoc></inheritdoc>
    public async Task UpdateAsync(Guid id, RecordLabel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Assertion.AssertIdIsSame(id, model);

        Context.RecordLabels.Update(model);

        await Context.SaveChangesAsync();
    }
}
