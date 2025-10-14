using SL.Abstraction;
using System;
using System.Collections.Generic;

namespace SL;

/// <summary>
/// Default implementation of a localizer that combines localization provider and selector functionality.
/// </summary>
public sealed class Localizer : ILocalizer, ILocalizationProvider, ILocalizationSelector
{
    private readonly Dictionary<string, ILocalizedString> m_Strings = [];
    private readonly Dictionary<string, ILocalization> m_Localizations = [];

    /// <inheritdoc/>
    public event LocalizationChangeHandler OnChanged;

    /// <inheritdoc/>
    public IEnumerable<ILocalization> Localizations => m_Localizations.Values;

    /// <inheritdoc/>
    public string CurrentLocalizationName
    {
        get;
        set
        {
            if(field == value) return;
            field = value;
            OnChanged?.Invoke(value, ChangeLocalization(value));
        }
    }

    /// <inheritdoc/>
    public ILocalization CurrentLocalization { get; private set; }

    /// <inheritdoc/>
    public ILocalizedString GetLocalizedString(string localizeKey) => m_Strings.TryGetValue(localizeKey, out var str) ? str : CreateLocalizedString(localizeKey);

    /// <inheritdoc/>
    public bool HasLocalization(string name) => m_Localizations.ContainsKey(name);

    /// <inheritdoc/>
    public ILocalization GetLocalization(string name) => m_Localizations[name];

    /// <inheritdoc/>
    public void AddLocalization(ILocalization localization) => m_Localizations.Add(localization.Name, localization);

    /// <inheritdoc/>
    public void RemoveLocalization(string localizationName) => _ = m_Localizations.Remove(localizationName);

    private ILocalization ChangeLocalization(string name) => CurrentLocalization = m_Localizations.TryGetValue(name, out var localization) ? localization : null;

    private LozalizedString CreateLocalizedString(string localizeKey)
    {
        LozalizedString str = new(this, localizeKey);
        m_Strings.Add(localizeKey, str);
        return str;
    }

    private sealed class LozalizedString(ILocalizationSelector localizer, string localizeKey) : ILocalizedString
    {
        private readonly string m_LocalizeKey = localizeKey;
        private readonly WeakReference<ILocalizationSelector> m_WeakLocalizer = new(localizer);

        public string Localize() => InternalLocalize() ?? m_LocalizeKey;

        public override string ToString() => Localize();

        private string InternalLocalize() => m_WeakLocalizer.TryGetTarget(out var localizer) ? localizer.CurrentLocalization?.Localize(m_LocalizeKey) : null;
    }
}