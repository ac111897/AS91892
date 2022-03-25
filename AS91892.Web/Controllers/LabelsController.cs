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
