using nietras.SeparatedValues;

namespace VerifyTests;

public static partial class VerifySep
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

    static void Scrub(StringBuilder builder, Counter arg2)
    {
        using var reader = Sep.Reader().FromText(builder.ToString());
        using var writer = reader.Spec.Writer().ToText();
        foreach (var sourceRow in reader)
        {
            using var targetRow = writer.NewRow();
            foreach (var colName in reader.Header.ColNames)
            {
                var sourceCell = sourceRow[colName];
                targetRow[colName].Set(sourceCell.Span);
            }
        }

        builder.Clear();
        builder.Append(writer);
    }

    public static void PagesToInclude(this VerifySettings settings, int count) =>
        settings.Context["VerifyDocNetPagesToInclude"] = count;


    public static SettingsTask PagesToInclude(this SettingsTask settings, int count)
    {
        settings.CurrentSettings.PagesToInclude(count);
        return settings;
    }

    static int GetPagesToInclude(this IReadOnlyDictionary<string, object> settings, int count)
    {
        if (settings.TryGetValue("VerifyDocNetPagesToInclude", out var value))
        {
            return Math.Min(count, (int)value);
        }

        return count;
    }
}