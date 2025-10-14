namespace SL.Abstraction;

/// <summary>
/// Represents a handler for localization update events.
/// </summary>
public delegate void LocalizationUpdateHandler();

/// <summary>
/// Represents a localization that provides string translation capabilities.
/// </summary>
public interface ILocalization
{
    /// <summary>
    /// Occurs when the localization data is updated.
    /// </summary>
    event LocalizationUpdateHandler OnUpdated;

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
    /// Updates the localization data from the specified source.
    /// </summary>
    /// <param name="source">The source containing the updated localization data.</param>
    void Update(ILocalizationSource source);
}