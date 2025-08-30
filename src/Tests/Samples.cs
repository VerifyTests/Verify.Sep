[TestFixture]
public class Samples
{
    #region VerifyCsv

    [Test]
    public Task VerifyCsv() =>
        VerifyFile("sample.csv");

    #endregion

    #region VerifyCsvStream

    [Test]
    public Task VerifyCsvStream()
    {
        var stream = File.OpenRead("sample.csv");
        return Verify(stream, "csv");
    }

    #endregion
}