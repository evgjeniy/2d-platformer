using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Utils;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private List<Locale> locales;
    
    public void Switch(string language) => 
        locales.Find(locale => locale.LocaleName.Contains(language.ToLower())).IfNotNull(Switch);

    private static void Switch(Locale locale) => LocalizationSettings.SelectedLocale = locale;
}