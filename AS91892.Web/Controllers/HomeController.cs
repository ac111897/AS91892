using AS91892.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AS91892.Web.Controllers;

/// <summary>
/// The home controller of the web application
/// </summary>
public class HomeController : Controller
{
    private static Dictionary<int, string> CodePaths { get; } = new()
    {
        { StatusCodes.Status404NotFound, "NotFound" },
        { StatusCodes.Status400BadRequest, "BadRequest" },
    };
    private ILogger<HomeController> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class
    /// </summary>
    /// <param name="logger"></param>
    public HomeController(ILogger<HomeController> logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// Returns the index view
    /// </summary>
    /// <returns>An index view</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Gets the privacy view
    /// </summary>
    /// <returns>A privacy view</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Reroutes erros to pages
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [Route("/Home/HandleError/{code:int}")]
    public IActionResult HandleError(int code)
    {
        if (CodePaths.ContainsKey(code))
        {
            return View(CodePaths[code]);
        }

        Logger.LogDebug("Server encoutered error code {code}", code);

        return View("HomeError", code);
    }


    /// <summary>
    /// Gets an <see cref="ErrorViewModel"/> view
    /// </summary>
    /// <returns>A <see cref="ErrorViewModel"/> view</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
