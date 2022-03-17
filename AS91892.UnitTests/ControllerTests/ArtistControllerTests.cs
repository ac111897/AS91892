﻿using AS91892.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AS91892.Tests.ControllerTests;
public class ArtistControllerTests
{
    
    [Fact]
    public async Task ArtistController_Should_Return_Elements()
    {
        ArtistsController controller = new(
            Creation.CreateLogger<ArtistsController>(),
            new ArtistRepository(await CreateContext()));

        var result = await controller.Index();

        Assert.True(result is ViewResult);
    }

    public async Task ArtistsController_Should_Return_NotFound()
    {
        ArtistsController controller = new(
            Creation.CreateLogger<ArtistsController>(),
            new ArtistRepository(await CreateContext()));

        var result = await controller.Details(Guid.Empty);


        Assert.True(result is NotFoundResult);
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