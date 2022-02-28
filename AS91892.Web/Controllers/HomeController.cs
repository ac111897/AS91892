using AS91892.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AS91892.Web.Controllers;

/// <summary>
/// The home controller of the web application
/// </summary>
public class HomeController : Controller
{

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

    [Route("Home/HandleError/{code:int}")]
    public async Task<IActionResult> HandleErrorAsync(int code)
    {
        if (code == 404)
        {
            return View("/NotFound");
        }
        return View();
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
