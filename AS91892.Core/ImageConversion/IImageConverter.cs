using AS91892.Data.Entities;
using AS91892.Data.ViewModels;

namespace AS91892.Core.ImageConversion;

/// <summary>
/// Converts an <see cref="ImageViewModel"/> to an <see cref="Image"/>
/// </summary>
public interface IImageConverter<in TID>
{
    /// <summary>
    /// Saves the <see cref="ImageViewModel"/> to <paramref name="directory"/>/<paramref name="fileName"/> and returns the file path and Id
    /// </summary>
    /// <param name="image">The image</param>
    /// <param name="directory"></param>
    /// <param name="fileName"></param>
    /// <returns><see cref="Task{TResult}"/> of <see cref="ImageViewModel"/> to <see langword="await"/></returns>
    Task<Image> ToImageAsync(ImageViewModel image, string directory, TID fileName);
}
