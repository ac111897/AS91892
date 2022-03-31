using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Song"/> model
/// </summary>
[Route("Songs")]
public class SongsController : ControllerWithRepo<SongsController, ISongRepository, Song>
{
    private IImageConverter<Guid> Converter { get; }
    private IWebHostEnvironment Environment { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SongsController"/>
    /// </summary>
    public SongsController(ILogger<SongsController> logger, ISongRepository repository, 
        IImageConverter<Guid> converter, IWebHostEnvironment environment) : base(logger, repository)
    {
        Converter = converter;
        Environment = environment;
    }


    /// <summary>
    /// Returns the index view of the controller
    /// </summary>
    /// <returns></returns>
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        return View(await Repository.GetAllAsync());
    }

    /// <summary>
    /// Creates a song in the repository
    /// </summary>
    /// <param name="song"></param>
    /// <returns></returns>
    [HttpPost, ActionName(nameof(Create))]
    [Route(nameof(Create))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync([Bind("")] SongViewModel song)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        Song actualObject = new()
        {
            Id = Guid.NewGuid(),
        };

        Image imageObject = await Converter.ToImageAsync(song.Image, Environment.WebRootPath, actualObject.Id);

        actualObject.Cover = imageObject;

        return View();
    }
}
