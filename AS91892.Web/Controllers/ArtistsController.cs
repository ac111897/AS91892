using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for artists
/// </summary>
[Route("artists")]
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
    public async Task<IActionResult> Index(Guid? id)
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
        await Repository.CreateAsync(artist);

        Logger.LogInformation("Created: {model}", artist);

        return View(artist);
    }

    /// <summary>
    /// Http post method for updating an <see cref="Artist"/>
    /// </summary>
    /// <param name="artist">The <see cref="Artist"/> to update</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="Artist"/> to <see langword="await"/></returns>
    [HttpPost]
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
}
