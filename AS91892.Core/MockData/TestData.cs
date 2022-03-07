using AS91892.Data.Entities;

namespace AS91892.Core.MockData;

public static class TestData 
{
    public static IEnumerable<Artist> GenerateMock()
    {
        return new List<Artist>()
        {
            new Artist() { Id = Guid.NewGuid(), ArtistName = "Something", Albums = new List<Album>(), Label = new RecordLabel() { Address = "26 Something Oakroad", Id = Guid.NewGuid(), Name = "Nice" } }
        };
    }
}
