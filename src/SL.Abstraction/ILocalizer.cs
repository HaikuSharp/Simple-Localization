namespace SL.Abstraction;

/// <summary>
/// Provides functionality for localizing string keys.
/// </summary>
public interface ILocalizer
{
    /// <summary>
    /// Creates a localized string for the specified key.
    /// </summary>
    /// <param name="localizeKey">The key to localize.</param>
    /// <returns>A localized string that will be translated based on the current localization.</returns>
    ILocalizedString Localize(string localizeKey);
}