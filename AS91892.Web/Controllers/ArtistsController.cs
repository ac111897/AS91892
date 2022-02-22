﻿using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;


/// <summary>
/// Controller for artists
/// </summary>
public class ArtistsController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArtistsController"/> class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="repository"></param>
    public ArtistsController(ILogger<ArtistsController> logger, IArtistRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }

    private ILogger<ArtistsController> Logger { get; }
    private IArtistRepository Repository { get; }

    /// <summary>
    /// Gets the main page
    /// </summary>
    /// <returns></returns>
    [Route("{id}")]
    public async Task<IActionResult> Index(Guid? id = null)
    {
        if (id is null)
        {
            return View(await Repository.GetAllAsync());
        }
        var model = await Repository.GetAsync(id.Value);

        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }

    /// <summary>
    /// Creates an <see cref="Artist"/> in the database
    /// </summary>
    /// <param name="artist"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Artist artist)
    {
        Logger.LogInformation("Created {model}", artist);

        await Repository.CreateAsync(artist);

        return View(artist);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Artist artist)
    {
        if (await Repository.GetAsync(artist.Id) is null)
        {
            
        }
    }
}
