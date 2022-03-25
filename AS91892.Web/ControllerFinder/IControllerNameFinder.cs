namespace AS91892.Web.ControllerFinder;

/// <summary>
/// Finds all the controllers currently registered in the application
/// </summary>
public interface IControllerNameFinder
{
    /// <summary>
    /// Gets the non appended controller name all controllers and returns them
    /// </summary>
    /// <returns>Every controller name</returns>
    public IEnumerable<string> GetControllerNames();
}
