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
    public event LocalizationLoadHandler OnLoaded;

    /// <inheritdoc/>
    public event LocalizationLoadHandler OnSaved;

    /// <inheritdoc/>
    public string Name => name;

    /// <inheritdoc/>
    public string Localize(string localizeKey) => m_Values.TryGetValue(localizeKey, out var localizedValue) ? localizedValue : localizeKey;

    /// <inheritdoc/>
    public void Load(ILocalizationSource source)
    {
        var values = m_Values;

        values.Clear();

        foreach(var rawLocalizedString in source.Values)
        {
            rawLocalizedString.Deconstruct(out var key, out var value);
            values.Add(key, value);
        }

        OnLoaded?.Invoke();
    }

    public void Save(ILocalizationSource source)
    {
        foreach(var kvp in m_Values) source.SetRaw(kvp.Key, kvp.Value);

        OnSaved?.Invoke();
    }
}