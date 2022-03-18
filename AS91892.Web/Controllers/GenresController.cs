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
    public async Task<IActionResult> CreateAsync(Genre genre)
    {
        await Repository.CreateAsync(genre);

        Logger.LogInformation("Created: {genre}", genre);

        return View(await Repository.GetAsync(genre.Id));
    }


    [Route("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var item = await Repository.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return View(item);
    }


    /// <summary>
    /// Confirms the deletion of a target resource from the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(Guid id)
    {
        await Repository.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
