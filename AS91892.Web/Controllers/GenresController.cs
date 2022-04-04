using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="Genre"/> model
/// </summary>
[Route("Genres")]
public class GenresController : ControllerWithRepo<GenresController, IGenreRepository, Genre>
{
    /// <inheritdoc></inheritdoc>
    public GenresController(ILogger<GenresController> logger, IGenreRepository repository) : base(logger, repository)
    {
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

        var genreSource = Repository.Source;


        var source = !string.IsNullOrEmpty(searchString) ?
            genreSource.Where(a => a.Title.ToLower().Contains(searchString.ToLower()))
            : genreSource;

        source = sortOrder switch
        {
            "name_desc" => source.OrderByDescending(x => x.Title),
            _ => source.OrderBy(x => x.Title),
        };

        int pageSize = 5;

        return View(await PaginatedList<Genre>.CreateAsync(source, pageNumber ?? 1, pageSize));
    }

    /// <summary>
    /// Creation end point on the <see cref="GenresController"/>
    /// </summary>
    /// <param name="genre"></param>
    /// <returns></returns>
    [Route(nameof(Create))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync([Bind("Title")] Genre genre)
    {
        genre.Id = Guid.NewGuid();


        await Repository.CreateAsync(genre);
        
        Logger.LogInformation("Created: {genre}", genre);


        return RedirectToAction(nameof(Details), new { id = genre.Id });
    }

    /// <summary>
    /// Updates the genre asynchrounously
    /// </summary>
    /// <param name="id"></param>
    /// <param name="genre"></param>
    /// <returns></returns>
    [Route(nameof(Update))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAsync(Guid id, [Bind("Id, Title")] Genre genre)
    {
        if (genre.Id != id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(nameof(Update));
        }

        await Repository.UpdateAsync(id, genre);

        return RedirectToAction(nameof(Details), new { id });
    }
}
