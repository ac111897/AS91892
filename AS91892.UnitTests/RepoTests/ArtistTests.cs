using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AS91892.Data.Context;
using AS91892.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AS91892.Tests.RepoTests;

public class ArtistTests
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
        IArtistRepository repo = new ArtistRepository(await CreateContext());
        var id = Guid.NewGuid();

        await repo.CreateAsync(new Artist() { Id = id, ArtistName = "Nice" });

        var obj = await repo.GetAsync(id);

        Assert.NotNull(obj);

        Assert.Equal("Nice", obj!.ArtistName);
    }
    

    // test that creating and updating the model should change the original record in the database

    [Fact]
    public async Task UpdateShould_Work()
    {
        IArtistRepository repo = new ArtistRepository(await CreateContext());

        var id = Guid.NewGuid();

        await repo.CreateAsync(new Artist() { Id = id, ArtistName = "Ok" });

        var createdObject = await repo.GetAsync(id);

        Assert.NotNull(createdObject);

        Assert.NotNull(createdObject?.ArtistName);

        createdObject!.ArtistName = "nice";

        await repo.UpdateAsync(id, createdObject);

        Assert.Equal("nice", createdObject.ArtistName);
    }

    // test that creating and then deleting said object should work

    [Fact]
    public async Task DeleteShould_Work()
    {
        IArtistRepository repo = new ArtistRepository(await CreateContext());

        var id = Guid.NewGuid();

        await repo.CreateAsync(new Artist() { Id = id, ArtistName = "some", Albums = new List<Album>()});

        Assert.NotNull(await repo.GetAsync(id));

        await repo.DeleteAsync(id);

        Assert.Null(await repo.GetAsync(id));
    }

    // test that the database should not be empty after adding data
    [Fact]
    public async Task ShouldHaveElementsInRepo()
    {
        IArtistRepository repo = new ArtistRepository(await CreateContext());

        Assert.NotEmpty(await repo.GetAllAsync());
    }

    [Fact]
    public async void ShouldBeDisposed()
    {
        IArtistRepository repo = new ArtistRepository(await CreateContext());

        repo?.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => repo!.CreateAsync(new()));
    }

    private async Task ExceptionThrowingMethod()
    {
        var repository = new ArtistRepository(await CreateContext());
        await repository.CreateAsync(null!);
    }
    private static async Task<ApplicationDbContext> CreateContext()
    {
        var context = Creation.CreateContext((x, r) => r.Artists.Add(x), GetDataset());

        await context.SaveChangesAsync();

        return context;
    }
    private static List<Artist> GetDataset()
    {
        return new List<Artist>()
        {
            new Artist() { Id = Guid.NewGuid(), ArtistName = "safsdfklsdfj", Albums = new List<Album>()},
            new Artist() { Id = Guid.NewGuid(), ArtistName = "somessafjd", Albums = new List<Album>()}

        };
    }
}
