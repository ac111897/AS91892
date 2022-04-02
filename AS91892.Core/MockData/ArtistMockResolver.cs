using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

public class ArtistMockResolver : MockResolverBase<Artist> 
{
    public ArtistMockResolver(ILogger<IMockDataResolver<Artist>> logger, IConfiguration configuration) 
        : base("/TestData/artists.json", logger, configuration) 
    { }
}

