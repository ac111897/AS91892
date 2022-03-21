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
    /// Creation end point on the <see cref="GenresController"/>
    /// </summary>
    /// <param name="genre"></param>
    /// <returns></returns>
    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync([Bind("Title")] Genre genre)
    {
        genre.Id = Guid.NewGuid();


        await Repository.CreateAsync(genre);
        
        Logger.LogInformation("Created: {genre}", genre);


        return RedirectToAction(nameof(Details), new { id = genre.Id });
    }
}
