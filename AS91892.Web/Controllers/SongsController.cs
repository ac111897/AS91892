using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Song"/> model
/// </summary>
[Route("Songs")]
public class SongsController : ControllerWithRepo<SongsController, ISongRepository, Song>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SongsController"/>
    /// </summary>
    public SongsController(ILogger<SongsController> logger, ISongRepository repository) : base(logger, repository)
    {
    }
}
