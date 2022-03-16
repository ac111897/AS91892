using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="RecordLabel"/> model
/// </summary>
public class LabelsController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LabelsController"/> class
    /// </summary>
    public LabelsController(ILogger<LabelsController> logger, ILabelRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }

    private ILogger<LabelsController> Logger { get; }
    private ILabelRepository Repository { get; }

    /// <summary>
    /// Index method of the <see cref="LabelsController"/> class
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Returns the details of the label
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("Details")]
    public async Task<IActionResult> Details(Guid id)
    {
        var model = await Repository.GetAsync(id);

        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }
}
