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
}
