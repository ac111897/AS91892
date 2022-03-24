﻿using AS91892.Web.Models;
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

    /// <summary>
    /// The path for when the client receives a bad request from the server
    /// </summary>
    /// <returns></returns>
    [Route("BadRequest")]
    public new IActionResult BadRequest()
    {
        return View();
    }

    /// <summary>
    /// Returns the not found page when the user enters an unknown route on the web application
    /// </summary>
    /// <returns></returns>
    [Route("NotFound")]
    public new IActionResult NotFound()
    {
        string originalPath = "unknown";
        if (HttpContext.Items.ContainsKey("originalPath"))
        {
            if (HttpContext.Items["originalPath"] is string value)
            {
                if (value is not null)
                {
                    originalPath = value;
                }
            }
        }

        ViewData["Path"] = originalPath;

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
