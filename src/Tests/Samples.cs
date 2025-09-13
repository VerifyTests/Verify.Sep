[TestFixture]
public class Samples
{
    #region VerifyCsv

    [Test]
    public Task VerifyCsv() =>
        VerifyFile("sample.csv");

    #endregion

    #region IgnoreColumns

    [Test]
    public Task IgnoreColumns() =>
        VerifyFile("sample.csv")
            .IgnoreCsvColumns("Customer Id");

    #endregion

    #region ScrubColumns

    [Test]
    public Task ScrubCsvColumns() =>
        VerifyFile("sample.csv")
            .ScrubCsvColumns("Customer Id");

    #endregion

    #region VerifyCsvStream

    [Test]
    public Task VerifyCsvStream()
    {
        var stream = File.OpenRead("sample.csv");
        return Verify(stream, "csv");
    }

    #endregion

    [Test]
    public Task VerifyReader()
    {
        using var source = Sep.Reader().FromFile("sample.csv");
        return Verify(source);
    }
}