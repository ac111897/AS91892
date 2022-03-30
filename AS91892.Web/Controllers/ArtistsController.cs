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
    /// Returns the index view of the controller
    /// </summary>
    /// <returns></returns>
    [Route("Index")]
    public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;

        if (searchString is not null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        ViewData["CurrentFiler"] = searchString;


        var artists = !string.IsNullOrEmpty(searchString) ? 
            await Repository.GetAllAsync(x => x.ArtistName.ToLower().Contains(searchString.ToLower())) 
            : await Repository.GetAllAsync();

        artists = sortOrder switch
        {
            "name_desc" => artists.OrderByDescending(x => x.ArtistName).ToList(),
            _ => artists.OrderBy(x => x.ArtistName).ToList(),
        };

        int pageSize = 5;

        return View(PaginatedList<Artist>.Create(artists, pageNumber ?? 1, pageSize));
    }

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
        Debug.WriteLine($"LabelId is {artist.LabelId}");
#endif
        artist.Id = Guid.NewGuid();

        if (!Guid.TryParse(artist.LabelId, out var labelId))
        {
            return BadRequest();    
        }

        artist.Label = labelId == Guid.Empty ? null : await LabelRepository.GetAsync(labelId);

        if (artist.Label is null)
        {
            return BadRequest();
        }

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

        if (!ModelState.IsValid)
        {
            return View(nameof(Update));
        }

        await Repository.UpdateAsync(artist.Id, artist);

        return RedirectToAction(nameof(Details), new { id = artist.Id });
    }
}
