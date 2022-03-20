﻿using Microsoft.AspNetCore.Mvc;
#if DEBUG
using System.Diagnostics;
#endif

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for artists
/// </summary>
[Route("Artists")]
public class ArtistsController : ControllerWithRepo<ArtistsController, IArtistRepository, Artist>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArtistsController"/> class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="repository"></param>
    public ArtistsController(ILogger<ArtistsController> logger, IArtistRepository repository) : base(logger, repository)
    {
    }


    /// <summary>
    /// Returns the create view
    /// </summary>
    /// <returns></returns>
    [Route("Create")]
    public IActionResult Create()
    {
#if DEBUG
        Debug.WriteLine("hit Artists/Create (stateless)");
#endif
        return View();
    }

    /// <summary>
    /// Creates an <see cref="Artist"/> in the database
    /// </summary>
    /// <param name="artist"></param>
    /// <returns></returns>
    [HttpPost, ActionName("Create")]
    [Route("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ArtistName")] Artist artist)
    {
#if DEBUG
        Debug.WriteLine("hit Artists/Create");
#endif
        artist.Id = Guid.NewGuid();

        if (ModelState.IsValid)
        {
#if DEBUG
            Debug.WriteLine("trying create Artists/Create");
#endif
            await Repository.CreateAsync(artist);
            Logger.LogInformation("Created {model}", artist);
            return RedirectToAction(nameof(Index));
        }
#if DEBUG
        Debug.WriteLine("returning Artists/Create");
#endif
        Logger.LogInformation("ModelState is {model}, Reasons: {reasons}", ModelState.ValidationState, string.Join(", ", ModelState.Values.Select(x => x.RawValue)));

        return View(artist);
    }

    /// <summary>
    /// Http post method for updating an <see cref="Artist"/>
    /// </summary>
    /// <param name="artist">The <see cref="Artist"/> to update</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="Artist"/> to <see langword="await"/></returns>
    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> Update(Artist artist)
    {
        if (await Repository.GetAsync(artist.Id) is null)
        {
            return View();
        }

        Logger.LogInformation("Updated: {model}", artist);

        await Repository.UpdateAsync(artist.Id, artist);

        return View(await Repository.GetAllAsync());
    }


    /// <summary>
    /// Returns the delete view
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await Repository.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return View(item);
    }


    /// <summary>
    /// Confirms the deletion of a target resource from the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Route("Delete")]
    public async Task<IActionResult> DeleteConfirmedAsync(Guid id)
    {
        await Repository.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
