using AS91892.Web.Models;
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
    private IImageConverter<Guid> Converter { get; }
    private IWebHostEnvironment Environment { get; }

    /// <inheritdoc></inheritdoc>
    public AlbumsController(ILogger<AlbumsController> logger, IAlbumRepository repository, 
        IImageConverter<Guid> converter, IWebHostEnvironment environment) : base(logger, repository)
    {
        Converter = converter;
        Environment = environment;
    }

    /// <summary>
    /// Returns the index view
    /// </summary>
    /// <returns></returns>
    [Route(nameof(Index))]
    public async Task<IActionResult> Index()
    {
        return View(await Repository.GetAllAsync());
    }

    /// <summary>
    /// Creation endpoint for the <see cref="AlbumsController"/> class
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(nameof(Create))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync([Bind("")] AlbumViewModel album)
    {
#if DEBUG
        Debug.WriteLine($"hit Albums/Create with {album}");
#endif
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        album.Id = Guid.NewGuid();

        var imageObject = await Converter.ToImageAsync(album.Photo, Environment.WebRootPath, album.Id);

        album.AlbumCover = imageObject;

        try
        {
            await Repository.CreateAsync(album);
        }
        catch
        {
            if (System.IO.File.Exists(imageObject.FilePath))
            {
                System.IO.File.Delete(imageObject.FilePath);
            }

            return RedirectToRoute(nameof(Index));
        }

       
        return View(nameof(Details), album);
    }

    /// <summary>
    /// Update endpoint for the <see cref="AlbumsController"/> class
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Update")]
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
