using AS91892.Data.Entities;
using AS91892.Data.ViewModels;

namespace AS91892.Core.ImageConversion;

/// <summary>
/// Saves an <see cref="ImageViewModel"/> into a file using the specified unique key and directory, then returns the path that is what is saved in and the image id to look for
/// </summary>
public class ImageConverter : IImageConverter<Guid>
{
    /// <inheritdoc></inheritdoc>
    public async Task<Image> ToImageAsync(ImageViewModel image, string directory, Guid fileName)
    {
        ArgumentNullException.ThrowIfNull(image, nameof(image));
        ArgumentNullException.ThrowIfNull(directory, nameof(directory));

        using var memoryStream = new MemoryStream();

        await image.Photo.CopyToAsync(memoryStream).ConfigureAwait(false);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string path = Path.Join(directory.AsSpan(), $"{fileName}.jpg");

        using var fileStream = File.Create(path);

        await memoryStream.CopyToAsync(fileStream).ConfigureAwait(false);

        return new Image() { FilePath = path, Id = fileName };
    }
}
