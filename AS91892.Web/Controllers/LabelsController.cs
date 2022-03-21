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
}
