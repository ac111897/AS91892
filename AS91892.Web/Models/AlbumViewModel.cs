namespace AS91892.Web.Models;

/// <summary>
/// Contains an <see cref="Album"/> and an <see cref="ImageViewModel"/>
/// </summary>
public class AlbumViewModel
{
    /// <summary>
    /// Containing album
    /// </summary>
    public Album Value { get; set; } = new();
    /// <summary>
    /// Provides a class to deal with an <see cref="IFormFile"/>
    /// </summary>
    public ImageViewModel Image { get; set; } = new();
}
