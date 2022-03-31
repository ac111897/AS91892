using System.ComponentModel.DataAnnotations;

namespace AS91892.Web.Models;

/// <summary>
/// Contains a <see cref="Song"/> and an <see cref="ImageViewModel"/>
/// </summary>
public class SongViewModel : Song
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SongViewModel"/> class
    /// </summary>
    public SongViewModel()
    {
        Duration = default;
        // set duration to default so we dont get model errors
    }
    /// <summary>
    /// Provides a class to deal with <see cref="IFormFile"/>
    /// </summary>
    [Required]
    public ImageViewModel Image { get; set; } = new();

#nullable disable
    
    /// <summary>
    /// The number of seconds in the duration
    /// </summary>
    [Display(Name = "Seconds")]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers are allowed")]
    [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Seconds must be a natural number")]
    [Required]
    public int Seconds { get; set; }

    /// <summary>
    /// The number of minutes in the duration
    /// </summary>
    [Display(Name = "Minutes")]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers are allowed")]
    [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Minutes must be a natural number")]
    [Required]
    public int Minutes { get; set; }


    /// <summary>
    /// The genre of the song
    /// </summary>
    [Display(Name = "Genre")]
    [Required]
    public string GenreId { get; set; }
}
