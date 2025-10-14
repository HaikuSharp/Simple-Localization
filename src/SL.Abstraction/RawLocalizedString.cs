using System;

namespace SL.Abstraction;

/// <summary>
/// Represents a raw localized string with a key and its corresponding translated value.
/// </summary>
public readonly struct RawLocalizedString(string key, string value) : IEquatable<RawLocalizedString>
{
    /// <summary>
    /// Gets the localization key.
    /// </summary>
    public string Key => key;

    /// <summary>
    /// Gets the localized value.
    /// </summary>
    public string Value => value;

    /// <summary>
    /// Deconstructs the raw localized string into its key and value components.
    /// </summary>
    /// <param name="key">The localization key.</param>
    /// <param name="value">The localized value.</param>
    public void Deconstruct(out string key, out string value)
    {
        key = Key;
        value = Value;
    }

    /// <inheritdoc/>
    public bool Equals(RawLocalizedString other) => Key == other.Key;

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is RawLocalizedString rawLocalizedString && Equals(rawLocalizedString);

    /// <inheritdoc/>
    public override int GetHashCode() => Key.GetHashCode();

    /// <summary>
    /// Returns the localized value as a string.
    /// </summary>
    /// <returns>The localized value.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Determines whether two <see cref="RawLocalizedString"/> instances have the same key.
    /// </summary>
    /// <param name="left">The first raw localized string to compare.</param>
    /// <param name="right">The second raw localized string to compare.</param>
    /// <returns>true if the keys are equal; otherwise, false.</returns>
    public static bool operator ==(RawLocalizedString left, RawLocalizedString right) => left.Equals(right);

    /// <summary>
    /// Determines whether two <see cref="RawLocalizedString"/> instances have different keys.
    /// </summary>
    /// <param name="left">The first raw localized string to compare.</param>
    /// <param name="right">The second raw localized string to compare.</param>
    /// <returns>true if the keys are not equal; otherwise, false.</returns>
    public static bool operator !=(RawLocalizedString left, RawLocalizedString right) => !(left == right);
}