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
        VerifierSettings.AddScrubber("csv", Handle);
        VerifierSettings.RegisterFileConverter<SepReader>(ConvertReader);
    }

    static ConversionResult ConvertReader(SepReader target, IReadOnlyDictionary<string, object> context)
    {
        var result = Convert(target, context, Counter.Current);
        return new(null, "csv", result);
    }

    static void Handle(StringBuilder builder, Counter counter, IReadOnlyDictionary<string, object> context)
    {
        using var source = Sep.Reader().FromText(builder.ToString());
        var value = Convert(source, context, counter);
        builder.Clear();
        builder.Append(value);
    }

    static string Convert(SepReader source, IReadOnlyDictionary<string, object> context, Counter counter)
    {
        using var target = source.Spec.Writer().ToText();
        var columns = GetColumns(source, context, counter).ToList();
        foreach (var sourceRow in source)
        {
            using var targetRow = target.NewRow();
            foreach (var (column, translate) in columns)
            {
                var sourceCell = sourceRow[column];
                var targetCell = targetRow[column];
                var translated = translate(sourceCell.Span.ToString());
                targetCell.Set(translated);
            }
        }

        target.Flush();

        return target.ToString();
    }

    static IEnumerable<(string column, Func<string, string> translate)> GetColumns(
        SepReader reader,
        IReadOnlyDictionary<string, object> context,
        Counter counter)
    {
        var translateBuilder = GetTranslate(context);

        var ignoreColumns = context.GetIgnoreCsvColumns();
        var scrubColumns = context.GetScrubCsvColumns();

        var columns = reader.Header.ColNames;

        foreach (var column in columns.Except(ignoreColumns))
        {
            var handle = GetHandle(scrubColumns, column, translateBuilder, counter);

            yield return (column, handle);
        }
    }

    static Func<string, string> translateScrubbed = _ => "{Scrubbed}";

    static Func<string, string> GetHandle(
        string[] scrubColumns,
        string column,
        Func<string, Func<string, string?>?>? translateBuilder,
        Counter counter)
    {
        if (scrubColumns.Contains(column))
        {
            return translateScrubbed;
        }

        var translate = translateBuilder?.Invoke(column);
        if (translate == null)
        {
            return _ =>
            {
                if (counter.TryConvert(_, out var result))
                {
                    return result;
                }

                return _;
            };
        }

        return _ => translate(_) ?? "null";
    }
}