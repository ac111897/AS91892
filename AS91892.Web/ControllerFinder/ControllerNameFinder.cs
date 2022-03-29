using System.Reflection;

namespace AS91892.Web.ControllerFinder;

/// <summary>
/// Finds all the controller names in the assembly
/// </summary>
public class ControllerNameFinder : IControllerNameFinder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ControllerNameFinder"/> class
    /// </summary>
    public ControllerNameFinder(ILogger<IControllerNameFinder> logger)
    {
        Logger = logger;
        Controllers = Initialise();
    }
    private List<string> Controllers { get; set; }
    private ILogger<IControllerNameFinder> Logger { get; }

    /// <summary>
    /// Initialises our finder
    /// </summary>
    public List<string> Initialise()
    {
        Logger?.LogInformation("Finding controller names");
        var assembly = Assembly.GetEntryAssembly();

        if (assembly is null)
        {
            throw new ArgumentException("Our executing assembly is not configured properly");
        }

        var list = assembly.GetTypes()
            .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type))
            .Where(type => type.GetMethod("Index") is not null)
            .Select(type => type.Name)
            .Where(name => name.EndsWith("Controller"))
            .Select(name => name.Replace("Controller", string.Empty))
            .ToList();

        list.MoveToFront(list.FindIndex(x => x == "Home"));

        return list;
    }
    /// <inheritdoc></inheritdoc>
    public IEnumerable<string> GetControllerNames()
    {
        return Controllers;
    }
}
