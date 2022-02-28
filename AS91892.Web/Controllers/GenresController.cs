using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Genre"/> model
/// </summary>
public class GenresController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenresController"/> class
    /// </summary>
    public GenresController(ILogger<GenresController> logger, IGenreRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }

    private ILogger<GenresController> Logger { get; }
    private IGenreRepository Repository { get; }


    /// <summary>
    /// Creation end point on the <see cref="GenresController"/>
    /// </summary>
    /// <param name="genre"></param>
    /// <returns></returns>
    [Route("create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(Genre genre)
    {
        await Repository.CreateAsync(genre);

        Logger.LogInformation("Created: {genre}", genre);

        return View(await Repository.GetAsync(genre.Id));
    }


    /// <summary>
    /// Index method of the <see cref="GenresController"/>
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }
}
