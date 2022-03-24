using System.IO;
using AS91892.Core.ImageConversion;
using Microsoft.AspNetCore.Http;

namespace AS91892.Tests;

public class ImageTests
{
    private const string FileName = "image.jpg";
    [Fact]
    public async Task CreateImage_WorksAsync()
    {
        IImageConverter<Guid> convert = new ImageConverter();

        string path = Path.Join(Directory.GetCurrentDirectory().AsSpan(), FileName);

        var image = File.OpenRead(path);


        IFormFile file = new FormFile(image, 0, image.Length, "Data", "image.jpg");

        ImageViewModel imageModel = new()
        {
            Photo = file
        };

        var id = Guid.NewGuid();

        string directory = Path.Join(Directory.GetCurrentDirectory(), "/img/");

        var saved = await convert.ToImageAsync(imageModel, directory, id);

        string outputPath = Path.Join(Directory.GetCurrentDirectory().AsSpan(), "/img/", $"{id}.jpg");

        Assert.True(outputPath == saved.FilePath);
        Assert.True(File.Exists(outputPath));
        Assert.True(!IsCorrupt(outputPath));
        Assert.True(new FileInfo(outputPath).Length > 0);
        
    }

    private static bool IsCorrupt(ReadOnlySpan<char> filePath)
    {
        var file = new FileInfo(filePath.ToString());

        try
        {
            if (FileAttributes.ReadOnly == file.Attributes)
            {
            }
            if (file.IsReadOnly)
            {

            }
            return false;
        }
        catch (Exception)
        {
            return true;
        }
    }
}
