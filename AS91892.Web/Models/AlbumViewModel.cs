using System.ComponentModel.DataAnnotations;
using AS91892.Data.Validation;

namespace AS91892.Web.Models;

/// <summary>
/// Contains an <see cref="Album"/>
/// </summary>
public class AlbumViewModel : Album
{
#nullable disable
    /// <summary>
    /// The photo for the album
    /// </summary>
    [Required(ErrorMessage = "Please select a file")]
    [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "File can not be larger than 10 megabytes")]
    [AllowedExtensions(new string[] { ".jpg" })]
    [DataType(DataType.Upload)]
    public IFormFile Photo { get; set; }
}
