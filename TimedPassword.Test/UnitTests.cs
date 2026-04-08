namespace Without.Systems.TimedPassword.Test;

public class Tests
{
    private static readonly ITimedPassword _actions = new TimedPassword();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GenerateAndVerify()
    {
        string code = _actions.Generate("loremipsumsecret");
        Assert.That(_actions.Verify(code, "loremipsumsecret"), Is.True);
    }
}