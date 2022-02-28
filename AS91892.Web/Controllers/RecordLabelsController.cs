using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// Controller for the <see cref="RecordLabel"/> model
/// </summary>
public class RecordLabelsController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecordLabelsController"/> class
    /// </summary>
    public RecordLabelsController(ILogger<RecordLabelsController> logger, ILabelRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }

    private ILogger<RecordLabelsController> Logger { get; }
    private ILabelRepository Repository { get; }

    /// <summary>
    /// Index method of the <see cref="RecordLabelsController"/> class
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }
}
