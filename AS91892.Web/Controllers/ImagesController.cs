using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Image"/> class
/// </summary>
[Route("Images")]
public class ImagesController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImagesController"/> class
    /// </summary>
    /// <param name="logger">An <see cref="ILogger{TCategoryName}"/> to be passed in by dependancy injection</param>
    /// <param name="repository">An <see cref="IImageRepository"/> to be passed in by dependancy injection</param>
    public ImagesController(ILogger<ImagesController> logger, IImageRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }

    private ILogger<ImagesController> Logger { get; }
    private IImageRepository Repository { get; }

    /// <summary>
    /// Returns the index view of the images controller
    /// </summary>
    /// <returns></returns>
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        return View(await Repository.GetAllAsync());
    }


    /// <summary>
    /// Details of an image - displays it
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("Details")]
    public async Task<IActionResult> Details(Guid id)
    {
        var image = await Repository.GetAsync(id);

        if (image is null)
        {
            return NotFound();
        }


        return View(image);
    }
}
