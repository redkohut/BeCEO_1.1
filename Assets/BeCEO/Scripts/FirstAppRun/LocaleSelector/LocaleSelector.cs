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
        // ��������� �� ������ ������� �������� � ����������
        yield return LocalizationSettings.InitializationOperation;
        // � �������� ����
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        active = false;
        // ��������� ����� �������� �����
        SceneLoader.LoadScene(1);

    }
    
   
}
