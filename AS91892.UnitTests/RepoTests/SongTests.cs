namespace AS91892.Tests.RepoTests;

/// <summary>
/// Tests for the song repository, methods are self-explanatory
/// </summary>
public class SongTests
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
        ISongRepository repo = new SongRepository(await CreateContext());
        var id = Guid.NewGuid();

        await repo.CreateAsync(new Song() 
        { 
            Genre = new Genre() { Id = Guid.NewGuid(), Title = "R&B"}, 
            Cover = null, 
            Features = null, 
            Id = id, 
            Title = "some song", 
            Duration = TimeSpan.FromMinutes(3.5)}
        );

        var obj = await repo.GetAsync(id);

        Assert.NotNull(obj);

        Assert.Equal("some song", obj!.Title);
    }


    // test that creating and updating the model should change the original record in the database

    [Fact]
    public async Task UpdateShould_Work()
    {
        ISongRepository repo = new SongRepository(await CreateContext());

        var id = Guid.NewGuid();

        await repo.CreateAsync(new Song()
        {
            Id = id,
            Cover = null,
            Duration = TimeSpan.FromSeconds(120),
            Features = new List<Artist>()
            {
                new Artist()
                {
                    ArtistName = "some artist",
                    Id = Guid.NewGuid(),
                    Albums = null,
                    Label = new RecordLabel()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Some label",
                        Address = "some address",
                    }
                }
            },
            Title = "nice",
            Genre = new Genre()
            {
                Id = Guid.NewGuid(),
                Title = "some genre"
            }
        });

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
        ISongRepository repo = new SongRepository(await CreateContext());

        var id = Guid.NewGuid();

        await repo.CreateAsync(new Song()
        {
            Id = id,
            Cover = null,
            Duration = TimeSpan.FromSeconds(120),
            Features = new List<Artist>()
            {
                new Artist()
                {
                    ArtistName = "some artist",
                    Id = Guid.NewGuid(),
                    Albums = null,
                    Label = new RecordLabel()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Some label",
                        Address = "some address"
                    }
                }
            },
            Title = "nice",
            Genre = new Genre()
            {
                Id = Guid.NewGuid(),
                Title = "some genre"
            }
        });

        Assert.NotNull(await repo.GetAsync(id));

        await repo.DeleteAsync(id);

        Assert.Null(await repo.GetAsync(id));
    }

    // test that the database should not be empty after adding data
    [Fact]
    public async Task ShouldHaveElementsInRepo()
    {
        ISongRepository repo = new SongRepository(await CreateContext());

        Assert.NotEmpty(await repo.GetAllAsync());
    }

    [Fact]
    public async void ShouldBeDisposed()
    {
        ISongRepository repo = new SongRepository(await CreateContext());

        repo?.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => repo!.CreateAsync(new()));
    }

    private async Task ExceptionThrowingMethod()
    {
        var repository = new SongRepository(await CreateContext());
        await repository.CreateAsync(null!);
    }
    private static async Task<ApplicationDbContext> CreateContext()
    {
        var context = Creation.CreateContext((x, r) => r.Songs.Add(x), GetDataset());

        await context.SaveChangesAsync();

        return context;
    }
    private static List<Song> GetDataset()
    {
        return new List<Song>()
        {
        new Song()
        {
            Id = Guid.NewGuid(),
            Cover = null,
            Duration = TimeSpan.FromSeconds(120),
            Features = new List<Artist>()
            {
                new Artist()
                {
                    ArtistName = "some artist",
                    Id = Guid.NewGuid(),
                    Albums = null,
                    Label = new RecordLabel()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Some label",
                        Address = "Some address",
                    }
                }
            },
            Title = "nice",
            Genre = new Genre()
            {
                Id = Guid.NewGuid(),
                Title = "some genre"
            }
        }
        };
    }
}
