using Microsoft.AspNetCore.Mvc;

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
    [Route("{id:guid}")]
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
}
