using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Song"/> model
/// </summary>
public class SongsController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SongsController"/>
    /// </summary>
    public SongsController(ILogger<SongsController> logger, ISongRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }

    private ILogger<SongsController> Logger { get; }
    private ISongRepository Repository { get; }

    /// <summary>
    /// Index method of the <see cref="SongsController"/>
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }
}
