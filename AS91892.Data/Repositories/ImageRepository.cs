namespace AS91892.Data.Repositories;


/// <summary>
/// Data repository <see cref="Image"/>
/// </summary>
public class ImageRepository : BaseRepository<ImageRepository>, IImageRepository
{
    /// <inheritdoc></inheritdoc>/>
    public IQueryable<Image> Source => Context.Images.AsNoTracking();

    /// <inheritdoc></inheritdoc>/>
    public ImageRepository(ApplicationDbContext context) : base(context)
    {
    }


    /// <inheritdoc></inheritdoc>
    public async Task<int> CountAsync(Expression<Func<Image, bool>> predicate)
    {
        return await Context.Images.CountAsync(predicate);
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<int> CountAsync()
    {
        return await Context.Images.CountAsync();
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task CreateAsync(Image model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Context.Images.Add(model);

        await Context.SaveChangesAsync();
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task DeleteAsync(Guid id)
    {
        var model = await Context.Images.FindAsync(id);

        if (model is null)
        {
            return;
        }

        Context.Images.Remove(model);

        await Context.SaveChangesAsync();
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<IList<Image>> GetAllAsync()
    {
        return await Task.FromResult(Context.Images.ToList());
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<IList<Image>> GetAllAsync(Func<Image, bool> predicate)
    {
        return await Task.FromResult(Context.Images.Where(predicate).ToList());
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task<Image?> GetAsync(Guid id)
    {
        return await Context.Images.FindAsync(id);
    }

    /// <inheritdoc></inheritdoc>/>
    public async Task UpdateAsync(Guid id, Image model)
    {
        ArgumentNullException.ThrowIfNull(model);

        Assertion.AssertIdIsSame(id, model);

        Context.Images.Update(model);

        await Context.SaveChangesAsync();
    }
}
