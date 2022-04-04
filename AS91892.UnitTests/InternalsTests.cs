namespace AS91892.Tests;

/// <summary>
/// General internals relating to the project tests
/// </summary>
public class InternalsTests
{
    [Fact]
    public void Assertion_ShouldThrow()
    {
        BaseEntity entity = new RecordLabel() { Id = Guid.NewGuid() };
        Assert.Throws<ArgumentException>(() => Assertion.AssertIdIsSame(Guid.NewGuid(), entity));    
    }
    [Fact]
    public void Assert_ShouldWork()
    {
        var id = Guid.NewGuid();
        BaseEntity entity = new RecordLabel() { Id = id };

        Assertion.AssertIdIsSame(id, entity);
    }
}
