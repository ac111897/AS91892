using Microsoft.AspNetCore.Mvc;
#if DEBUG
using System.Diagnostics;
#endif

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Album"/> model
/// </summary>
public class AlbumsController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AlbumsController"/> class
    /// </summary>
    public AlbumsController(ILogger<AlbumsController> logger, IAlbumRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }

    private ILogger<AlbumsController> Logger { get; }
    private IAlbumRepository Repository { get; }


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


    /// <summary>
    /// Index method of the <see cref="AlbumsController"/>
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
#if DEBUG
        Debug.WriteLine("Hit Albums/Index");
#endif
        return View();
    }
}
