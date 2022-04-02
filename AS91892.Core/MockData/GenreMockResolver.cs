using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

public class GenreMockResolver : MockResolverBase<Genre>
{
    public GenreMockResolver(ILogger<IMockDataResolver<Genre>> logger, IConfiguration configuration) : base("/TestData/genres.json", logger, configuration)
    { 
    }
}

