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
        using var source = Sep.Reader().FromText(builder.ToString());
        using var target = source.Spec.Writer().ToText();
        var columns = GetColumns(source,context).ToList();
        foreach (var sourceRow in source)
        {
            using var targetRow = target.NewRow();
            foreach (var (column, translate) in columns)
            {
                var sourceCell = sourceRow[column];
                var targetCell = targetRow[column];
                targetCell.Set(translate(sourceCell.Span.ToString()));
            }
        }

        target.Flush();

        builder.Clear();

        builder.Append(target);
    }
    static Func<string, string> translateScrubbed = _ => "Scrubbed";
    static IEnumerable<(string column, Func<string, string> translate)> GetColumns(SepReader reader, IReadOnlyDictionary<string, object> context)
    {
        Func<string, Func<string, string?>?>? translateBuilder = null;
        if (context.TryGetValue("VerifySepCsvTranslate", out var builderValue))
        {
            translateBuilder = (Func<string, Func<string, string>?>) builderValue;
        }

        var ignoreColumns = context.GetIgnoreCsvColumns();
        var scrubColumns = context.GetScrubCsvColumns();
        var columns = reader.Header.ColNames;
        columns = columns.Except(ignoreColumns).ToArray();
        foreach (var column in columns)
        {
            if (scrubColumns.Contains(column))
            {
                yield return (column, translateScrubbed);
                continue;
            }

            var translate = translateBuilder?.Invoke(column);
            if (translate != null)
            {
                yield return (column, _ => translate(_) ?? "null");
                continue;
            }

            yield return (column, _ => _);
        }
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