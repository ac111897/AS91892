using System.ComponentModel.DataAnnotations;

namespace AS91892.Web.Models;

/// <summary>
/// Contains a <see cref="Song"/> and an <see cref="ImageViewModel"/>
/// </summary>
public class SongViewModel : Song
{
    /// <summary>
    /// Provides a class to deal with <see cref="IFormFile"/>
    /// </summary>
    [Required]
    public ImageViewModel Image { get; set; } = new();

#nullable disable
    /// <summary>
    /// The genre of the song
    /// </summary>
    [Display(Name = "Genre")]
    [Required]
    public string GenreId { get; set; }
}
