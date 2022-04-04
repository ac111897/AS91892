using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

/// <summary>
/// Mock resolver to get <see cref="Album"/> data
/// </summary>
public class AlbumMockResolver : MockResolverBase<Album>
{
    public AlbumMockResolver(ILogger<IMockDataResolver<Album>> logger, IConfiguration config) 
        : base("TestData/albums.json", logger, config)
    {
    }
}
