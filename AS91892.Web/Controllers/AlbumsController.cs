using Microsoft.AspNetCore.Mvc;

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
        await Repository.CreateAsync(album);

        Logger.LogInformation("Created {album}", album);


        return View(await Repository.GetAsync(album.Id));

    }

    [HttpPost]
    public async Task<IActionResult> UpdateAsync(Album album)
    {
        if (await Repository.GetAsync(album.Id) is null)
        {
            return View();
        }
        return View();
    }


    /// <summary>
    /// Index method of the <see cref="AlbumsController"/>
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }
}
