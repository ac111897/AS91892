using AS91892.Web.Models;
using Microsoft.AspNetCore.Mvc;
#if DEBUG
using System.Diagnostics;
#endif

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Album"/> model
/// </summary>
[Route("Albums")]
public class AlbumsController : ControllerReadOnly<AlbumsController, IAlbumRepository, Album>
{
    private IImageConverter<Guid> Converter { get; }
    private IWebHostEnvironment Environment { get; }
    private IArtistRepository ArtistRepository { get; }

    /// <inheritdoc></inheritdoc>
    public AlbumsController(ILogger<AlbumsController> logger, IAlbumRepository repository, 
        IImageConverter<Guid> converter, IWebHostEnvironment environment,
        IArtistRepository artistRepository) : base(logger, repository)
    {
        Converter = converter;
        Environment = environment;
        ArtistRepository = artistRepository;
    }

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


        var albumSource = Repository.Source;


        var source = !string.IsNullOrEmpty(searchString) ?
            albumSource.Where(a => a.Title.ToLower().Contains(searchString.ToLower()))
            : albumSource;

        source = sortOrder switch
        {
            "name_desc" => source.OrderByDescending(x => x.Title),
            _ => source.OrderBy(x => x.Title),
        };

        int pageSize = 5;

        return View(await PaginatedList<Album>.CreateAsync(source, pageNumber ?? 1, pageSize));
    }

    /// <summary>
    /// Returns the create view of the albums controller
    /// </summary>
    /// <param name="id">The route parameter id</param>
    /// <returns>A view of the create page</returns>
    [HttpGet]
    [ActionName("CreateView")]
    [Route("CreateView")]
    public async Task<IActionResult> Create(Guid id)
    {
        var artist = await ArtistRepository.GetAsync(id);

        if (artist is null)
        {
            return NotFound();
        }

        ViewData["ArtistId"] = id;

        return View("Create");
    }


    /// <summary>
    /// Update endpoint for album class
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("Update")]
    public override async Task<IActionResult> Update(Guid id)
    {
        var album = await Repository.GetAsync(id);

        if (album is null)
        {
            return NotFound();
        }

        return View(new AlbumViewModel()
        {
            Id = album.Id,
            AlbumSongs = album.AlbumSongs,
            Title = album.Title,
            Year = album.Year,
            AlbumCover = album.AlbumCover
        });
    }


    /// <summary>
    /// Creation endpoint for the <see cref="AlbumsController"/> class
    /// </summary>
    /// <param name="id"></param>
    /// <param name="album"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(Guid id, [Bind("Title, Year, Photo")] AlbumViewModel album)
    {
#if DEBUG
        Debug.WriteLine($"hit Albums/Create with {album}");
#endif

        album.Id = Guid.NewGuid();
        album.AlbumSongs = new List<Song>();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var imageObject = await Converter.ToImageAsync(album.Photo, Path.Join(Environment.WebRootPath, "/img"), album.Id);

        album.AlbumCover = imageObject;

        var artist = await ArtistRepository.GetAsync(id);
         
        if (artist is null)
        {
            return NotFound();
        }

        try
        {
            artist.Albums.Add(album);

            await ArtistRepository.UpdateAsync(id, artist);
        }
        catch
        {
            if (System.IO.File.Exists(imageObject.FilePath))
            {
                System.IO.File.Delete(imageObject.FilePath);
            }

            return RedirectToRoute(nameof(Index));
        }


        return RedirectToAction(nameof(Details), new { id = album.Id });
    }


    /// <summary>
    /// Update endpoint for the <see cref="AlbumsController"/> class
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> UpdateAsync(Album album)
    {
#if DEBUG
        Debug.WriteLine($"hit Albums/Update with {album}");
#endif
        if (await Repository.GetAsync(album.Id) is null)
        {
            return View();
        }

        if (ModelState.IsValid)
        {
            Logger.LogInformation("Updated model with id of {id}", album.Id);

            await Repository.UpdateAsync(album.Id, album);

            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}
