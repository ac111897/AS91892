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
public abstract class ControllerWithRepo<TController, TRepository, TModel> : ControllerReadOnly<TController, TRepository, TModel> 
    where TController : Controller
    where TModel : BaseEntity
    where TRepository : IRepository<TModel, Guid>
{

    /// <summary>
    /// Initializes a new instance of the 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="repository"></param>
    public ControllerWithRepo(ILogger<TController> logger, TRepository repository) : base(logger, repository)
    {

    }

    /// <summary>
    /// Returns the create view to the user
    /// </summary>
    /// <returns></returns>
    [Route("Create")]
    public IActionResult Create()
    {
        return View();
    }
}
