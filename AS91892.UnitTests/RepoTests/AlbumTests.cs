namespace AS91892.Tests.RepoTests;

public class AlbumTests
{
    [Fact]
    public async Task CreateShouldFail_OnNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(ExceptionThrowingMethod);
    }

    // assert that creating should modidy the database

    [Fact]
    public async Task CreateShould_Work()
    {
        IAlbumRepository repo = new AlbumRepository(await CreateContext());
        var id = Guid.NewGuid();

        await repo.CreateAsync(new Album() { Id = id, AlbumCover = null, AlbumSongs = new List<Song>() { }, Title = "Some test album", Year = DateTime.Now.Year });

        var obj = await repo.GetAsync(id);

        Assert.NotNull(obj);

        Assert.Equal("Some test album", obj!.Title);
    }


    // test that creating and updating the model should change the original record in the database

    [Fact]
    public async Task UpdateShould_Work()
    {
        IAlbumRepository repo = new AlbumRepository(await CreateContext());

        var id = Guid.NewGuid();

        await repo.CreateAsync(new Album() { Id = id, Title = "new album" });

        var createdObject = await repo.GetAsync(id);

        Assert.NotNull(createdObject);

        Assert.NotNull(createdObject?.Title);

        createdObject!.Title = "nice";

        await repo.UpdateAsync(id, createdObject);

        Assert.Equal("nice", createdObject.Title);
    }

    // test that creating and then deleting said object should work

    [Fact]
    public async Task DeleteShould_Work()
    {
        IAlbumRepository repo = new AlbumRepository(await CreateContext());

        var id = Guid.NewGuid();

        await repo.CreateAsync(new Album() { Id = id, Title = "some", AlbumCover = null, Year = DateTime.Now.Year, AlbumSongs = new List<Song>() });

        Assert.NotNull(await repo.GetAsync(id));

        await repo.DeleteAsync(id);

        Assert.Null(await repo.GetAsync(id));
    }

    // test that the database should not be empty after adding data
    [Fact]
    public async Task ShouldHaveElementsInRepo()
    {
        IAlbumRepository repo = new AlbumRepository(await CreateContext());

        Assert.NotEmpty(await repo.GetAllAsync());
    }

    [Fact]
    public async void ShouldBeDisposed()
    {
        IAlbumRepository repo = new AlbumRepository(await CreateContext());

        repo?.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => repo!.CreateAsync(new()));
    }

    private async Task ExceptionThrowingMethod()
    {
        var repository = new AlbumRepository(await CreateContext());
        await repository.CreateAsync(null!);
    }
    private static async Task<ApplicationDbContext> CreateContext()
    {
        var context = Creation.CreateContext((x, r) => r.Albums.Add(x), GetDataset());

        await context.SaveChangesAsync();

        return context;
    }
    private static List<Album> GetDataset()
    {
        return new List<Album>()
        {
            new Album() { Id = Guid.NewGuid(), AlbumCover = null, AlbumSongs = new List<Song>(), Title = "some album", Year = DateTime.Now.Year },
            new Album() { Id = Guid.NewGuid(), AlbumCover = null, AlbumSongs = new List<Song>(), Title = "some other album", Year = DateTime.Now.Year },
        };
    }
}
