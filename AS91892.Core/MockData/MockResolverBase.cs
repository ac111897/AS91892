using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AS91892.Core.MockData;

public abstract class MockResolverBase<T> : IMockDataResolver<T>
    where T : BaseEntity
{
    public MockResolverBase(string fileName, ILogger<IMockDataResolver<T>> logger, IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(fileName, nameof(fileName));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(config, nameof(config));

        DataFile = fileName;
        Logger = logger;
        Config = config;
    }

    private string DataFile { get; }

    private ILogger<IMockDataResolver<T>> Logger { get; }
    private IConfiguration Config { get; }

    public IEnumerable<T> GenerateMock()
    {
        ReadOnlySpan<char> subFolder = Path.Join(Config["Example-Data-Path"], DataFile);

        ReadOnlySpan<char> path = Path.Join(Directory.GetCurrentDirectory().AsSpan(), subFolder);


        using var streamReader = new StreamReader(path.ToString());

        ReadOnlySpan<char> jsonData = streamReader.ReadToEnd();

        try
        {
            return JsonSerializer.Deserialize<T[]>(jsonData) ?? Array.Empty<T>();
        }
        catch (Exception exception)
        {
            Logger.LogError("Failed to retreive mock data from data.json \n {ex}", exception);
            return Array.Empty<T>();
        }
    }

}
