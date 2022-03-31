using System.ComponentModel.DataAnnotations;
using AS91892.Data.Validation;

namespace AS91892.Web.Models;

/// <summary>
/// Contains a <see cref="Song"/> and an <see cref="IFormFile"/>
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

#nullable disable

    /// <summary>
    /// The photo for the song
    /// </summary>
    [Required(ErrorMessage = "Please select a file")]
    [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "File can not be larger than 10 megabytes")]
    [AllowedExtensions(new string[] { ".jpg" })]
    [DataType(DataType.Upload)]
    public IFormFile Photo { get; set; }

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
