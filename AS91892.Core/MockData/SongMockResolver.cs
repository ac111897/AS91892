using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

public class SongMockResolver : MockResolverBase<Song>
{
    public SongMockResolver(ILogger<IMockDataResolver<Song>> logger, IConfiguration config) 
        : base("/TestData/songs.json", logger, config)
    {
    }
}

