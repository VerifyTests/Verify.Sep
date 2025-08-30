namespace VerifyTests;

public static class VerifySep
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.AddScrubber("csv", Scrub);
    }

    static void Scrub(StringBuilder builder, Counter counter, IReadOnlyDictionary<string, object> context)
    {
        var ignoreColumns = context.GetIgnoreCsvColumns();
        var scrubColumns = context.GetScrubCsvColumns();
        using var source = Sep.Reader().FromText(builder.ToString());
        using var target = source.Spec.Writer().ToText();

        var colNames = source.Header.ColNames;
        colNames = colNames.Except(ignoreColumns).ToArray();

        foreach (var sourceRow in source)
        {
            using var targetRow = target.NewRow();
            foreach (var colName in colNames)
            {
                var sourceCell = sourceRow[colName];
                var targetCell = targetRow[colName];
                if (scrubColumns.Contains(colName))
                {
                    targetCell.Set("Scrubbed");
                }
                else
                {
                    targetCell.Set(sourceCell.Span);
                }
            }
        }

        target.Flush();

        builder.Clear();
        builder.Append(target);
    }

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

    public static void IgnoreCsvColumns(this VerifySettings settings, params string[] columns) =>
        settings.Context["VerifySepIgnoreColumns"] = columns;

    public static SettingsTask IgnoreCsvColumns(this SettingsTask settings, params string[] columns)
    {
        settings.CurrentSettings.IgnoreCsvColumns(columns);
        return settings;
    }

    static string[] GetIgnoreCsvColumns(this IReadOnlyDictionary<string, object> settings)
    {
        if (settings.TryGetValue("VerifySepIgnoreColumns", out var value))
        {
            return (string[]) value;
        }

        return [];
    }
}