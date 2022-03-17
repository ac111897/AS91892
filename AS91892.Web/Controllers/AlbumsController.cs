using Microsoft.AspNetCore.Mvc;
#if DEBUG
using System.Diagnostics;
#endif

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Album"/> model
/// </summary>
[Route("Albums")]
public class AlbumsController : ControllerWithRepo<AlbumsController, IAlbumRepository, Album>
{
    /// <inheritdoc></inheritdoc>
    public AlbumsController(ILogger<AlbumsController> logger, IAlbumRepository repository) : base(logger, repository)
    {
    }


    /// <summary>
    /// Creation endpoint for the <see cref="AlbumsController"/> class
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(Album album)
    {
#if DEBUG
        Debug.WriteLine($"hit Albums/Create with {album}");
#endif
        await Repository.CreateAsync(album);

        Logger.LogInformation("Created {album}", album);


        return View(await Repository.GetAsync(album.Id));

    }

    /// <summary>
    /// Update endpoint for the <see cref="AlbumsController"/> class
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> UpdateAsync(Album album)
    {
#if DEBUG
        Debug.WriteLine($"hit Albums/Update with {album}");
#endif
        if (await Repository.GetAsync(album.Id) is null)
        {
            return View();
        }

        if (ModelState.IsValid)
        {
            Logger.LogInformation("Updated model with id of {id}", album.Id);

            await Repository.UpdateAsync(album.Id, album);

            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}
