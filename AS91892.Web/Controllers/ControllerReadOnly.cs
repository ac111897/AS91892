using Microsoft.AspNetCore.Mvc;

namespace AS91892.Web.Controllers;

/// <summary>
/// A base controller that sets up the logger and the repository, as well as the index view and the details view
/// </summary>
/// <typeparam name="TController">The controller that our base class is getting implemented from, this is used for the Logger and the comments</typeparam>
/// <typeparam name="TRepository">The type of repository that the controller should implement, should implement <see cref="IRepository{TModel, TModelID}"/></typeparam>
/// <typeparam name="TModel">This should be the same model that the <typeparamref name="TRepository"/> works with</typeparam>
/// <remarks>
/// This abstract class is used to make our code cleaner and not write repetitive code as each class 
/// implements the same index method and details method logic so there is no need to write more than needed
/// <code>
/// // controllers have to specify routes or it will use '/'
/// [Route("<typeparamref name="TModel"/>")]
/// public class TModelController : <see cref="ControllerWithRepo{TController, TRepository, TModel}"/>
/// </code>
/// </remarks>
public abstract class ControllerReadOnly<TController, TRepository, TModel> : Controller
    where TController : Controller
    where TModel : BaseEntity
    where TRepository : IRepository<TModel, Guid>
{
    /// <summary>
    /// Logger for the controller
    /// </summary>
    protected ILogger<TController> Logger { get; }

    /// <summary>
    /// Data access for the controller
    /// </summary>
    protected TRepository Repository { get; }

    /// <summary>
    /// Initializes a new instance of the <typeparamref name="TController"/> class with a Logger and a <typeparamref name="TRepository"/>
    /// </summary>
    /// <param name="logger">A logger for the class</param>
    /// <param name="repository">A repository used for data access for the model</param>
    public ControllerReadOnly(ILogger<TController> logger, TRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        Logger = logger;
        Repository = repository;
    }


    /// <summary>
    /// Returns the details of a particular object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("Details")]
    public async Task<IActionResult> Details(Guid id)
    {
        var item = await Repository.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return View(item);
    }

    /// <summary>
    /// Returns the delete view
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await Repository.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return View(item);
    }

    /// <summary>
    /// Returns the update view
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route(nameof(Update))]
    [HttpGet]
    public virtual async Task<IActionResult> Update(Guid id)
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
    [Route("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(Guid id)
    {
        await Repository.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
