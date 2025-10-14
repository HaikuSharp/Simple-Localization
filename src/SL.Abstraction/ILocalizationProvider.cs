using System.Collections.Generic;

namespace SL.Abstraction;

/// <summary>
/// Provides functionality for managing multiple localizations.
/// </summary>
public interface ILocalizationProvider
{
    /// <summary>
    /// Gets all available localizations.
    /// </summary>
    IEnumerable<ILocalization> Localizations { get; }

    /// <summary>
    /// Determines whether a localization with the specified name exists.
    /// </summary>
    /// <param name="name">The name of the localization to check.</param>
    /// <returns>true if the localization exists; otherwise, false.</returns>
    bool HasLocalization(string name);

    /// <summary>
    /// Gets the localization with the specified name.
    /// </summary>
    /// <param name="name">The name of the localization to retrieve.</param>
    /// <returns>The localization instance.</returns>
    ILocalization GetLocalization(string name);

    /// <summary>
    /// Adds a new localization to the provider.
    /// </summary>
    /// <param name="localization">The localization to add.</param>
    void AddLocalization(ILocalization localization);

    /// <summary>
    /// Removes the localization with the specified name.
    /// </summary>
    /// <param name="localizationName">The name of the localization to remove.</param>
    void RemoveLocalization(string localizationName);
}