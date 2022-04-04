using AS91892.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AS91892.Core.MockData;

/// <summary>
/// Mock resolver to get <see cref="RecordLabel"/> data
/// </summary>
public class LabelMockResolver : MockResolverBase<RecordLabel>
{
    public LabelMockResolver(ILogger<IMockDataResolver<RecordLabel>> logger, IConfiguration configuration)
        : base("/TestData/labels.json", logger, configuration)
    { }
}
