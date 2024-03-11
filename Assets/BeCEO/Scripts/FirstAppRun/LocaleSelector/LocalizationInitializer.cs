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
        // �������� ���
        localeID = SaveSystemSimple.GetLocaleID();
        // ���������� ������������ �������
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int localeID)
    {
        // ��������� �� ������ ������� �������� � ����������
        yield return LocalizationSettings.InitializationOperation;
        // � �������� ����
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
}
