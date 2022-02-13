using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;


/// <summary>
/// Controller for artists
/// </summary>
[Route("[controller]")]
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
    public async Task<IActionResult> Index()
    {
        return View(await Repository.GetAllAsync());
    }

    
}
