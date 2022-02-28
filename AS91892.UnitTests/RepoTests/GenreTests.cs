namespace AS91892.Tests.RepoTests;

public class GenreTests
{
    [Fact]
    public async Task CreateShouldFail_OnNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(ExceptionThrowingMethod);
    }

    [Fact]
    public async Task Create_ShouldWork()
    {
        var repo = await CreateRepoAsync();

        var id = Guid.NewGuid();

        await repo.CreateAsync(new Genre() { Id = id, Title = "Rap" });

        var item = await repo.GetAsync(id);

        Assert.NotNull(item);

        Assert.Equal("Rap", item!.Title);
    }


    private async Task ExceptionThrowingMethod()
    {
        var repository = await CreateRepoAsync();
        await repository.CreateAsync(null!);
    }

    private static async Task<IGenreRepository> CreateRepoAsync()
    {
        return new GenreRepository(await CreateContext());
    }
    private static async Task<ApplicationDbContext> CreateContext()
    {
        var context = Creation.CreateContext((x, r) => r.Genres.Add(x), GetDataset());

        await context.SaveChangesAsync();

        return context;
    }
    private static List<Genre> GetDataset()
    {
        return new List<Genre>()
        {
            new Genre() { Id = Guid.NewGuid(), Title = "Rock"},
            new Genre() { Id= Guid.NewGuid(), Title = "R&B"}
        };
    }
}
