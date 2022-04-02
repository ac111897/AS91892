using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

public class LabelMockResolver : MockResolverBase<RecordLabel>
{
    public LabelMockResolver(ILogger<IMockDataResolver<RecordLabel>> logger, IConfiguration configuration)
        : base("/TestData/artists.json", logger, configuration)
    { }
}


