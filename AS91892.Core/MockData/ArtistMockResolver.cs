using AS91892.Data.Entities;

namespace AS91892.Core.MockData;

public class ArtistMockResolver : IMockDataResolver<Artist>
{
    public IEnumerable<Artist> GenerateMock(int amount)
    {
        throw new NotImplementedException();
    }
}
