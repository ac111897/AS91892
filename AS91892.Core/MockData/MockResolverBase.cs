using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AS91892.Core.MockData;

/// <summary>
/// Base class to get data from a json file and deserialize it into our type
/// </summary>
/// <typeparam name="T">The type of the model</typeparam>
public abstract class MockResolverBase<T> : IMockDataResolver<T>
    where T : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IMockDataResolver{TEntity}"/> implementation class
    /// </summary>
    /// <param name="fileName">The name of the json file holding the data</param>
    /// <param name="logger">Logger to log errors that might occur</param>
    /// <param name="config"></param>
    public MockResolverBase(string fileName, ILogger<IMockDataResolver<T>> logger, IConfiguration config)
    {
        // check if any of are arguments are null to catch exceptions before they happen
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

    /// <inheritdoc></inheritdoc>
    public IEnumerable<T> GenerateMock()
    {
        // get the full path of where the file is
        ReadOnlySpan<char> subFolder = Path.Join(Config["Example-Data-Path"], DataFile);

        ReadOnlySpan<char> path = Path.Join(Directory.GetCurrentDirectory().AsSpan(), subFolder);

        // read the data from the stream into a string

        using var streamReader = new StreamReader(path.ToString());

        ReadOnlySpan<char> jsonData = streamReader.ReadToEnd(); 

        // try deserialize the data into the object class
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
