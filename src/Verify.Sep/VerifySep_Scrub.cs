namespace VerifyTests;

public static partial class VerifySep
{
    public static void ScrubCsvColumns(this VerifySettings settings, params string[] columns) =>
        settings.Context["VerifySepScrubColumns"] = columns;

    public static SettingsTask ScrubCsvColumns(this SettingsTask settings, params string[] columns)
    {
        settings.CurrentSettings.ScrubCsvColumns(columns);
        return settings;
    }

    static string[] GetScrubCsvColumns(this IReadOnlyDictionary<string, object> settings)
    {
        if (settings.TryGetValue("VerifySepScrubColumns", out var value))
        {
            return (string[]) value;
        }

        return [];
    }
}