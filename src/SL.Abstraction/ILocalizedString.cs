namespace SL.Abstraction;

/// <summary>
/// Represents a localized string that can be dynamically translated.
/// </summary>
public interface ILocalizedString
{
    /// <summary>
    /// Gets the localized string value based on the current localization.
    /// </summary>
    /// <returns>The localized string.</returns>
    string Localize();
}