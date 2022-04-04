using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

/// <summary>
/// Mock resolver to get <see cref="Genre"/> data
/// </summary>
public class GenreMockResolver : MockResolverBase<Genre>
{
    public GenreMockResolver(ILogger<IMockDataResolver<Genre>> logger, IConfiguration configuration) : base("/TestData/genres.json", logger, configuration)
    { 
    }
}
