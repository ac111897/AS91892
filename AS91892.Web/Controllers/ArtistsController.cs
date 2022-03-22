using AS91892.Web.Models;
using Microsoft.AspNetCore.Mvc;
#if DEBUG
using System.Diagnostics;
#endif

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for artists
/// </summary>
[Route("Artists")]
public class ArtistsController : ControllerWithRepo<ArtistsController, IArtistRepository, Artist>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArtistsController"/> class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="repository"></param>
    /// <param name="labelRepository"></param>
    public ArtistsController(ILogger<ArtistsController> logger, IArtistRepository repository, ILabelRepository labelRepository) : base(logger, repository)
    {
        LabelRepository = labelRepository;
    }

    private ILabelRepository LabelRepository { get; }

    /// <summary>
    /// Creates an <see cref="Artist"/> in the database
    /// </summary>
    /// <param name="artist"></param>
    /// <returns></returns>
    [HttpPost, ActionName(nameof(Create))]
    [Route(nameof(Create))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ArtistName, LabelId")] ArtistViewModel artist)
    {
#if DEBUG
        Debug.WriteLine("hit Artists/Create");
#endif
        artist.Id = Guid.NewGuid();

        artist.Label = artist.LabelId != Guid.Empty ? await LabelRepository.GetAsync(artist.LabelId) : null;

        artist.Albums = new List<Album>();

        var completeArtist = new Artist()
        {
            Id = artist.Id,
            ArtistName = artist.ArtistName,
            Label = artist.Label
        };

        if (ModelState.IsValid)
        {
#if DEBUG
            Debug.WriteLine("trying create Artists/Create");
#endif
            await Repository.CreateAsync(completeArtist);
            Logger.LogInformation("Created {model}", completeArtist);
            return RedirectToAction(nameof(Index));
        }
#if DEBUG
        Debug.WriteLine("returning Artists/Create");
#endif
        Logger.LogInformation("ModelState is {model}, Reasons: {reasons}", ModelState.ValidationState, string.Join(", ", ModelState.Values.Select(x => x.RawValue)));

        return View(artist);
    }

    /// <summary>
    /// Http post method for updating an <see cref="Artist"/>
    /// </summary>
    /// <param name="artist">The <see cref="Artist"/> to update</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="Artist"/> to <see langword="await"/></returns>
    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> Update(Artist artist)
    {
        if (await Repository.GetAsync(artist.Id) is null)
        {
            return View();
        }

        Logger.LogInformation("Updated: {model}", artist);

        await Repository.UpdateAsync(artist.Id, artist);

        return RedirectToAction(nameof(Details), new { id = artist.Id });
    }
}
