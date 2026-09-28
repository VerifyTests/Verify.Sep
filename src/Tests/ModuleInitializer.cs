public static class ModuleInitializer
{
    #region enable

    [ModuleInitializer]
    public static void Initialize() =>
        VerifySep.Initialize();

    #endregion

    [ModuleInitializer]
    public static void InitializeOther()
    {
        // Date scrubbing of the csv cells depends on the current culture's date format
        var culture = CultureInfo.GetCultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.CurrentCulture = culture;
        VerifyDiffPlex.Initialize();
    }
}