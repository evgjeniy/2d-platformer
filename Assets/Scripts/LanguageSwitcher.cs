using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private Locale defaultLocale;
    [SerializeField] private List<Locale> locales;

    public void Switch(string language)
    {
        var locale = locales.Find(locale => locale.LocaleName.Contains(language.ToLower()));
        Switch(locale == null ? defaultLocale : locale);
    }

    private static void Switch(Locale locale) => LocalizationSettings.SelectedLocale = locale;
}