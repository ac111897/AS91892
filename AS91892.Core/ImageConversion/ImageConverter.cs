using AS91892.Data.Entities;
using AS91892.Data.ViewModels;

namespace AS91892.Core.ImageConversion;

public class ImageConverter : IImageConverter<Guid>
{
    public async Task<Image> ToImageAsync(AlbumViewModel image, string directory, Guid fileName)
    {
        ArgumentNullException.ThrowIfNull(image, nameof(image));
        ArgumentNullException.ThrowIfNull(directory, nameof(directory));

        using var memoryStream = new MemoryStream();

        await image.Photo.CopyToAsync(memoryStream).ConfigureAwait(false);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string path = Path.Join(directory.AsSpan(), fileName.ToString() + ".jpg");

        using var fileStream = File.Create(path);

        await memoryStream.CopyToAsync(fileStream).ConfigureAwait(false);

        return new Image() { FilePath = path, Id = fileName };
    }
}
