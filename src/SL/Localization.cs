using SL.Abstraction;
using System.Collections.Generic;

namespace SL;

/// <summary>
/// Default implementation of a localization that stores translated strings in memory.
/// </summary>
public sealed class Localization(string name) : ILocalization
{
    private readonly Dictionary<string, string> m_Values = [];

    /// <inheritdoc/>
    public string Name => name;

    /// <inheritdoc/>
    public event LocalizationUpdateHandler OnUpdated;

    /// <inheritdoc/>
    public string Localize(string localizeKey) => m_Values.TryGetValue(localizeKey, out var localizedValue) ? localizedValue : localizeKey;

    /// <inheritdoc/>
    public void Update(ILocalizationSource source)
    {
        var values = m_Values;

        values.Clear();

        foreach(var rawLocalizedString in source.Values)
        {
            rawLocalizedString.Deconstruct(out var key, out var value);
            values.Add(key, value);
        }

        OnUpdated?.Invoke();
    }
}