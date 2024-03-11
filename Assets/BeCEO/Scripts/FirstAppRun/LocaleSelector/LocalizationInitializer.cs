using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationInitializer : MonoBehaviour
{
    [NonSerialized]
    private int localeID;
    // Start is called before the first frame update
    void Start()
    {
        // Отримаємо дані
        localeID = SaveSystemSimple.GetLocaleID();
        // ініціалізуємо локалізаційну систему
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int localeID)
    {
        // перевіримо чи готова функція введення в локалізацію
        yield return LocalizationSettings.InitializationOperation;
        // і виберемо мову
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
}
