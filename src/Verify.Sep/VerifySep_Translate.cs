namespace VerifyTests;

public static partial class VerifySep
{
    public static void TranslateCsvColumns(this VerifySettings settings, Func<string, Func<string, string>?> translate) =>
        settings.Context["VerifySepCsvTranslate"] = translate;

    public static SettingsTask TranslateCsvColumn(this SettingsTask settings, Func<string, Func<string, string>?> translate)
    {
        settings.CurrentSettings.TranslateCsvColumns(translate);
        return settings;
    }

    static Func<string, Func<string, string?>?>? GetTranslate(IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("VerifySepCsvTranslate", out var value))
        {
            return (Func<string, Func<string, string>?>) value;
        }

        return null;
    }
}