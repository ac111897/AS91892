namespace AS91892.Web.Models;

/// <summary>
/// Contains a <see cref="Song"/> and an <see cref="ImageViewModel"/>
/// </summary>
public class SongViewModel
{
    /// <summary>
    /// Containing song
    /// </summary>
    public Song Value { get; set; } = new();

    /// <summary>
    /// Provides a class to deal with <see cref="IFormFile"/>
    /// </summary>
    public ImageViewModel Image { get; set; } = new();
}
