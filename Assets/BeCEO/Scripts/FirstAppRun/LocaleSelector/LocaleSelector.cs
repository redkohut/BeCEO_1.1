using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        if (active)
        {
            return;
        }
        SaveSystemSimple.SetLocaleID(localeID);
        StartCoroutine(SetLocale(localeID));
    }
    IEnumerator SetLocale(int localeID)
    {
        // перевіримо чи готова функція введення в локалізацію
        yield return LocalizationSettings.InitializationOperation;
        // і виберемо мову
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        active = false;
        // загрузимо зразу наступну сцену
        SceneLoader.LoadScene(1);

    }
    
   
}
