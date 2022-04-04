using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Song"/> model
/// </summary>
[Route("Songs")]
public class SongsController : ControllerReadOnly<SongsController, ISongRepository, Song>
{
    private IImageConverter<Guid> Converter { get; }
    private IWebHostEnvironment Environment { get; }
    private IGenreRepository GenreRepository { get; }
    private IAlbumRepository AlbumRepository { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SongsController"/>
    /// </summary>
    public SongsController(ILogger<SongsController> logger, ISongRepository repository, 
        IImageConverter<Guid> converter, IWebHostEnvironment environment, 
        IGenreRepository genreRepository, IAlbumRepository albumRepository) : base(logger, repository)
    {
        Converter = converter;
        Environment = environment;
        GenreRepository = genreRepository;
        AlbumRepository = albumRepository;
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

        var songSource = Repository.Source;


        var source = !string.IsNullOrEmpty(searchString) ?
            songSource.Where(a => a.Title.ToLower().Contains(searchString.ToLower()))
            : songSource;

        source = sortOrder switch
        {
            "name_desc" => source.OrderByDescending(x => x.Title),
            _ => source.OrderBy(x => x.Title),
        };

        int pageSize = 5;

        return View(await PaginatedList<Song>.CreateAsync(source, pageNumber ?? 1, pageSize));
    }

    /// <summary>
    /// Returns the create view of the songs controller
    /// </summary>
    /// <param name="id">The route parameter id</param>
    /// <returns>A view of the create page</returns>
    [HttpGet]
    [ActionName("CreateView")]
    [Route("CreateView")]
    public async Task<IActionResult> Create(Guid id)
    {
        var artist = await AlbumRepository.GetAsync(id);

        if (artist is null)
        {
            return NotFound();
        }

        ViewData["AlbumId"] = id;

        return View("Create");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("Update")]
    public override async Task<IActionResult> Update(Guid id)
    {
        var song = await Repository.GetAsync(id);

        if (song is null)
        {
            return NotFound();
        }

        return View(new SongViewModel()
        {
            Cover = song.Cover,
            Duration = song.Duration,
            Features = song.Features,
            Genre = song.Genre,
            Id = song.Id,
            Minutes = song.Duration.Minutes,
            Seconds = song.Duration.Seconds,
            Title = song.Title,
        });
    }

    /// <summary>
    /// Creates a song in the repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="song"></param>
    /// <returns></returns>
    [HttpPost, ActionName(nameof(Create))]
    [Route(nameof(Create))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(Guid id, [Bind("Title, Seconds, Minutes, GenreId, Photo")] SongViewModel song)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!Guid.TryParse(song.GenreId.AsSpan(), out var genreId))
        {
            return BadRequest(ModelState);
        }

        var genre = await GenreRepository.GetAsync(genreId);


        var album = await AlbumRepository.GetAsync(id);

        if (album is null)
        {
            return NotFound();
        }

        song.Id = Guid.NewGuid();
        song.Duration = new TimeSpan(0, song.Minutes, song.Seconds);

        Image imageObject = await Converter.ToImageAsync(song.Photo, Path.Join(Environment.WebRootPath, "/img"), song.Id);

        song.Cover = imageObject;

        album!.AlbumSongs.Add(song);

        await AlbumRepository.UpdateAsync(album.Id, album);

        return RedirectToAction(nameof(Details), new { id = song.Id });
    }
}
