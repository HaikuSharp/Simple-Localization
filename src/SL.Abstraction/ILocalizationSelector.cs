namespace SL.Abstraction;

/// <summary>
/// Represents a handler for localization change events.
/// </summary>
/// <param name="name">The name of the new current localization.</param>
public delegate void LocalizationChangeHandler(string name);

/// <summary>
/// Provides functionality for selecting and managing the current localization.
/// </summary>
public interface ILocalizationSelector
{
    /// <summary>
    /// Occurs when the current localization changes.
    /// </summary>
    event LocalizationChangeHandler OnChanged;

    /// <summary>
    /// Gets or sets the name of the current localization.
    /// </summary>
    string CurrentLocalizationName { get; set; }

    /// <summary>
    /// Gets the current localization instance.
    /// </summary>
    ILocalization CurrentLocalization { get; }
}