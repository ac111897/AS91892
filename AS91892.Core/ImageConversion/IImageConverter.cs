using AS91892.Data.Entities;
using AS91892.Data.ViewModels;

namespace AS91892.Core.ImageConversion;

/// <summary>
/// Converts an <see cref="AlbumViewModel"/> to an <see cref="Image"/>
/// </summary>
public interface IImageConverter<in TID>
{
    Task<Image> ToImageAsync(AlbumViewModel image, string directory, TID fileName);
}
