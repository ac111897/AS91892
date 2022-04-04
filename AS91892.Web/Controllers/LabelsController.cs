using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="RecordLabel"/> model
/// </summary>
[Route("Labels")]
public class LabelsController : ControllerWithRepo<LabelsController, ILabelRepository, RecordLabel>
{
    /// <inheritdoc></inheritdoc>
    public LabelsController(ILogger<LabelsController> logger, ILabelRepository repository) : base(logger, repository)
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

        var labelSource = Repository.Source;


        var source = !string.IsNullOrEmpty(searchString) ?
            labelSource.Where(a => a.Name.ToLower().Contains(searchString.ToLower()))
            : labelSource;

        source = sortOrder switch
        {
            "name_desc" => source.OrderByDescending(x => x.Name),
            _ => source.OrderBy(x => x.Name),
        };

        int pageSize = 5;

        return View(await PaginatedList<RecordLabel>.CreateAsync(source, pageNumber ?? 1, pageSize));
    }

    /// <summary>
    /// Endpoint to create a record label in the database
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName(nameof(Create))]
    [ValidateAntiForgeryToken]
    [Route(nameof(Create))]
    public async Task<IActionResult> CreateAsync([Bind("Name, Address")] RecordLabel label)
    {
        label.Id = Guid.NewGuid();

        await Repository.CreateAsync(label);

        Logger.LogInformation("Created {label}", label);



        return RedirectToAction(nameof(Details), new { id = label.Id });
    }

    /// <summary>
    /// Updates a record in the database asynchronously
    /// </summary>
    /// <param name="id"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    [Route(nameof(Update))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAsync(Guid id, [Bind("Id, Name, Address")] RecordLabel label)
    {
        if (label.Id != id || label.Id == Guid.Empty)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(nameof(Update));
        }

        await Repository.UpdateAsync(id, label);

        return RedirectToAction(nameof(Details), new { id });
    }
}
