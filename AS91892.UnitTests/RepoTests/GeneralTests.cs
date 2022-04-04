namespace AS91892.Tests.RepoTests;

/// <summary>
/// General tests relating to the repositories, methods are self-explanatory
/// </summary>
public class GeneralTests
{
    [Fact]
    public async Task Artists_IsLinkedToAlbum()
    {
        var context = await CreateContext();
        IArtistRepository repo = new ArtistRepository(context);

        Album album = new() { AlbumCover = null, Id = Guid.NewGuid(), Title = "Nice" };

        Artist artist = new() { ArtistName = "nice", Id = Guid.NewGuid(), Albums = new List<Album>() { album } };

        await repo.CreateAsync(artist);

        Assert.NotNull(await context.Albums.FindAsync(album.Id));
    }
    
    private static async Task<ApplicationDbContext> CreateContext()
    {
        var context = Creation.CreateContext((x, r) => r.Artists.Add(x), GetDataset());

        await context.SaveChangesAsync();

        return context;
    }
    private static List<Artist> GetDataset() => new()
        {
            new Artist() 
            { Id = Guid.NewGuid(), ArtistName = "safsdfklsdfj", Albums = new List<Album>()
            },
            new Artist() { Id = Guid.NewGuid(), ArtistName = "somessafjd", Albums = new List<Album>()}

        };
}
