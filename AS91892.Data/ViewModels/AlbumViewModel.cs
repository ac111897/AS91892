using Microsoft.AspNetCore.Http;

namespace AS91892.Data.ViewModels;

#nullable disable

/// <summary>
/// View model to upload the image to the server and store the reference for later use
/// </summary>
public class AlbumViewModel : IValidatableObject
{
    /// <summary>
    /// The photo for the album
    /// </summary>
    [Required(ErrorMessage = "Please select a file")]
    [DataType(DataType.Upload)]
    public IFormFile Photo { get; set; }

    /// <inheritdoc></inheritdoc>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var photo = ((AlbumViewModel)validationContext.ObjectInstance).Photo;
        var extension = Path.GetExtension(photo.FileName);
        var size = photo.Length;

        if (!extension.ToLower().Equals(".jpg"))
        {
            yield return new ValidationResult("File extension is not valid.");
        }

        int maxFileSizeInMegabytes = 10;

        if (size > (maxFileSizeInMegabytes * 1024 * 1024))
        {
            yield return new ValidationResult($"File size is bigger than {maxFileSizeInMegabytes}MB.");
        }
    }
}
