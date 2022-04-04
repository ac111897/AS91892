using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

/// <summary>
/// Mock resolver to get <see cref="Artist"/> data
/// </summary>
public class ArtistMockResolver : MockResolverBase<Artist> 
{
    public ArtistMockResolver(ILogger<IMockDataResolver<Artist>> logger, IConfiguration configuration) 
        : base("/TestData/artists.json", logger, configuration) 
    { }
}

