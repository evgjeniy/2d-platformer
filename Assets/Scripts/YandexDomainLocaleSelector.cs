using Agava.YandexGames;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[System.Serializable]
public class YandexDomainLocaleSelector : IStartupLocaleSelector
{
    public Locale GetStartupLocale(ILocalesProvider availableLocales)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return null;
#endif
        var internationalization = YandexGamesSdk.Environment.i18n;
        var language = internationalization.tld.ToLower().Contains("com") ? "en" : internationalization.lang;

        return availableLocales.Locales.Find(locale => locale.LocaleName.Contains(language.ToLower()));
    }
}