using System.Collections.Generic;

namespace SL.Abstraction;

/// <summary>
/// Represents a source of localization data containing key-value pairs for translations.
/// </summary>
public interface ILocalizationSource
{
    /// <summary>
    /// Gets all raw localized strings from the source.
    /// </summary>
    IEnumerable<RawLocalizedString> Values { get; }

    /// <summary>
    /// Gets the raw localized string for the specified key.
    /// </summary>
    /// <param name="localizeKey">The key to retrieve.</param>
    /// <returns>The raw localized string.</returns>
    RawLocalizedString GetRaw(string localizeKey);

    /// <summary>
    /// Sets the localized value for the specified key.
    /// </summary>
    /// <param name="localizeKey">The key to set.</param>
    /// <param name="localizeValue">The localized value.</param>
    void SetRaw(string localizeKey, string localizeValue);

    /// <summary>
    /// Removes the localized string for the specified key.
    /// </summary>
    /// <param name="localizeKey">The key to remove.</param>
    void RemoveRaw(string localizeKey);

    /// <summary>
    /// Clears all localized strings from the source.
    /// </summary>
    void Clear();
}