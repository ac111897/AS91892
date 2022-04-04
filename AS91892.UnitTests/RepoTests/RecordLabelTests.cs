namespace AS91892.Tests.RepoTests;

/// <summary>
/// Tests for the record label repository, method are self-explanatory
/// </summary>
public class RecordLabelTests
{
    [Fact]
    public async Task AssertShouldFailOnNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(Exception_ThrowingMethod);
    }

    private static async Task Exception_ThrowingMethod()
    {
        ILabelRepository repository = new LabelRepository(await CreateContext());

        await repository.CreateAsync(null!);
    }

    private static async Task<ApplicationDbContext> CreateContext()
    {
        var context = Creation.CreateContext((x, r) => r.RecordLabels.Add(x), GetDataset());

        await context.SaveChangesAsync();

        return context;
    }

    private static List<RecordLabel> GetDataset()
    {
        return new List<RecordLabel>()
        {
            new() { Id = Guid.NewGuid(), Name = "fjldksfas;dfas", Address = "32 Oakland Road"},
            new() { Id = Guid.NewGuid(), Name = "Teyiare", Address = "52 Efland Road"}

        };
    }
}
