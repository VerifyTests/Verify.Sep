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
        using var source = Sep.Reader().FromText(builder.ToString());
        using var target = source.Spec.Writer().ToText();
        foreach (var sourceRow in source)
        {
            using var targetRow = target.NewRow();
            foreach (var colName in source.Header.ColNames)
            {
                var sourceCell = sourceRow[colName];
                targetRow[colName].Set(sourceCell.Span);
            }
        }
        target.Flush();

        builder.Clear();
        builder.Append(target);
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