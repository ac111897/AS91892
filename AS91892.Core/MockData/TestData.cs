using AS91892.Data.Entities;

namespace AS91892.Core.MockData;

public static class TestData 
{
    public static IEnumerable<Artist> Generate()
    {
        return new List<Artist>()
        {
            new Artist() { Id = Guid.NewGuid(), ArtistName = "Something", Albums = new List<Album>() }
        };
    }
}
