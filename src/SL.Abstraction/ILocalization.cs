namespace SL.Abstraction;

/// <summary>
/// Represents a handler for localization load events.
/// </summary>
public delegate void LocalizationLoadHandler();

/// <summary>
/// Represents a handler for localization save events.
/// </summary>
public delegate void LocalizationSaveHandler();

/// <summary>
/// Represents a localization that provides string translation capabilities.
/// </summary>
public interface ILocalization
{
    /// <summary>
    /// Occurs when the localization data is loaded.
    /// </summary>
    event LocalizationLoadHandler OnLoaded;

    /// <summary>
    /// Occurs when the localization data is saved.
    /// </summary>
    event LocalizationLoadHandler OnSaved;

    /// <summary>
    /// Gets the name of the localization.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Localizes the specified key to the corresponding translated string.
    /// </summary>
    /// <param name="localizeKey">The key to localize.</param>
    /// <returns>The localized string, or the original key if no translation is found.</returns>
    string Localize(string localizeKey);

    /// <summary>
    /// Loads the localization data from the specified source.
    /// </summary>
    /// <param name="source">The source.</param>
    void Load(ILocalizationSource source);

    /// <summary>
    /// Saves the localization data to the specified source.
    /// </summary>
    /// <param name="source">The source.</param>
    void Save(ILocalizationSource source);
}