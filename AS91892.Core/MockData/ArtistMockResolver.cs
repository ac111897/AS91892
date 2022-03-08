using AS91892.Data.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AS91892.Core.MockData;

public class ArtistMockResolver : IMockDataResolver<Artist>
{
    public ArtistMockResolver(ILogger<IMockDataResolver<Artist>> logger)
    {
        Logger = logger;
    }

    private ILogger<IMockDataResolver<Artist>> Logger { get; }

    public IEnumerable<Artist> GenerateMock()
    {
        using var streamReader = new StreamReader(Path.Join(Directory.GetCurrentDirectory().AsSpan(), "data.json"));

        ReadOnlySpan<char> jsonData = streamReader.ReadToEnd();

        try
        {
            return JsonSerializer.Deserialize<Artist[]>(jsonData) ?? Array.Empty<Artist>();
        }
        catch (Exception exception)
        {
            Logger.LogError("Failed to retreive mock data from data.json \n {ex}", exception);
            return Array.Empty<Artist>();
        }

    }
}
